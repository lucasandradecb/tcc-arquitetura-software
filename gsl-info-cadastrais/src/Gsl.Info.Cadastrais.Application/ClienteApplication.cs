using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Domain.Resources;
using Gsl.Info.Cadastrais.Domain.ValueObjects;
using Flunt.Notifications;
using System.Collections.Generic;

namespace Gsl.Info.Cadastrais.Application
{
    /// <summary>
    /// Classe de application de cliente
    /// </summary>
    public class ClienteApplication : IClienteApplication
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="clienteRepository"></param>
        public ClienteApplication(IMapper mapper, 
                                  IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de clientes 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<ClienteModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaClientes = await _clienteRepository.ListarTodos(ctx);

            return Result<List<ClienteModel>>.Ok(_mapper.Map<List<ClienteModel>>(listaClientes));
        }

        /// <summary>
        /// Obtem dados de um cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<ClienteModel>> ObterCliente(string cpf, CancellationToken ctx)
        {
            var output = new ClienteModel();

            var cpfCliente = new CPF(cpf);
            if (cpfCliente.Valid)
            {
                var cliente = await _clienteRepository.ObterPorCpf(cpfCliente.Numero, ctx);
                if (cliente != null)
                {
                    output = _mapper.Map<Cliente, ClienteModel>(cliente);
                    return Result<ClienteModel>.Ok(output);
                }
            }

            var notification = new List<Notification> { new Notification(nameof(Cliente.Cpf), MensagensInfo.Cliente_NaoEncontrado) };
            return Result<ClienteModel>.Error(notification);
            
        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Cliente>> CadastrarCliente(ClienteModel clienteModel, CancellationToken ctx)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);

            if (cliente.Cpf.Invalid)
                cliente.AddNotifications(cliente.Cpf.Notifications);

            if (cliente.Valid)
            {
                if (!await _clienteRepository.VerificarSeExiste(cliente, ctx))
                {
                    await _clienteRepository.Salvar(cliente, ctx);
                    return Result<Cliente>.Ok(cliente);
                }

                cliente.AddNotification(nameof(Cliente.Cpf), MensagensInfo.Cliente_CpfExistente);
            }

            return Result<Cliente>.Error(cliente.Notifications);
        }

        #endregion

    }
}
