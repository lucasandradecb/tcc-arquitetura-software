using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Domain.Resources;
using Gsl.Info.Cadastrais.Domain.ValueObjects;

namespace Gsl.Info.Cadastrais.Application
{
    /// <summary>
    /// Classe de application de cliente
    /// </summary>
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly IOperadorRepository _operadorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="clienteRepository"></param>
        /// <param name="operadorRepository"></param>
        /// <param name="usuarioRepository"></param>
        public UsuarioApplication(IMapper mapper, 
                                  IClienteRepository clienteRepository,
                                  IOperadorRepository operadorRepository,
                                  IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _operadorRepository = operadorRepository;
            _usuarioRepository = usuarioRepository;
        }

        #region Usuario

        /// <summary>
        /// Realiza o cadastro de um usuario
        /// </summary>
        /// <param name="usuarioModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<UsuarioModel>> CadastrarUsuario(UsuarioModel usuarioModel, CancellationToken ctx)
        {
            var usuario = _mapper.Map<UsuarioModel, Usuario>(usuarioModel);

            if (usuario.Valid)
            {
                if (!await _usuarioRepository.VerificarSeExiste(usuario, ctx))
                {
                    await _usuarioRepository.Salvar(usuario, ctx);
                    var output = _mapper.Map<UsuarioModel>(usuario);
                    return Result<UsuarioModel>.Ok(output);
                }

                usuario.AddNotification(nameof(Usuario), MensagensInfo.Usuario_LoginExistente);
            }

            return Result<UsuarioModel>.Error(usuario.Notifications);
        }

        /// <summary>
        /// Obtem dados de um usuario
        /// </summary>
        /// <param name="usuarioModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<UsuarioTipoModel>> ObterUsuario(UsuarioModel usuarioModel, CancellationToken ctx)
        {
            var usuario = _mapper.Map<UsuarioModel, Usuario>(usuarioModel);
            var output = new UsuarioTipoModel();

            if (usuario.Invalid)            
                return Result<UsuarioTipoModel>.Error(usuario.Notifications);

            var usuarioExistente = await _usuarioRepository.ObterPorLogin(usuario.Login, ctx);
            if (usuarioExistente == null)
            {
                usuario.AddNotification(nameof(Usuario), MensagensInfo.Usuario_NaoExiste);
                return Result<UsuarioTipoModel>.Error(usuario.Notifications);
            }

            if (usuario.Senha != usuarioExistente.Senha)
            {
                usuario.AddNotification(nameof(Usuario), MensagensInfo.Usuario_SenhaInvalida);
                return Result<UsuarioTipoModel>.Error(usuario.Notifications);
            }

            var cpf = new CPF(usuario.Login);
            if (cpf.Valid)
            {
                var cliente = await _clienteRepository.ObterPorCpf(usuarioModel.Login, ctx);
                if (cliente == null)
                {
                    usuario.AddNotification(nameof(Usuario), MensagensInfo.Usuario_ClienteNaoExiste);
                    return Result<UsuarioTipoModel>.Error(usuario.Notifications);
                }

                output.Cliente = _mapper.Map<Cliente, ClienteModel>(cliente);
            }
            else
            {
                var operador = await _operadorRepository.ObterPorMatricula(usuarioModel.Login, ctx);
                if (operador == null)
                {
                    usuario.AddNotification(nameof(Usuario), MensagensInfo.Usuario_OperadorNaoExiste);
                    return Result<UsuarioTipoModel>.Error(usuario.Notifications);
                }

                output.Operador = _mapper.Map<Operador, OperadorModel>(operador);
            }

            return Result<UsuarioTipoModel>.Ok(output);
        }

        #endregion

        #region Cliente

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

                cliente.AddNotification(nameof(Cliente), MensagensInfo.Cliente_CpfExistente);
            }

            return Result<Cliente>.Error(cliente.Notifications);
        }

        #endregion

        #region Operador

        /// <summary>
        /// Realiza o cadastro de um operador
        /// </summary>
        /// <param name="operadorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Operador>> CadastrarOperador(OperadorModel operadorModel, CancellationToken ctx)
        {
            var operador = _mapper.Map<OperadorModel, Operador>(operadorModel);

            if (operador.Valid)
            {
                if (!await _operadorRepository.VerificarSeExiste(operador, ctx))
                {
                    await _operadorRepository.Salvar(operador, ctx);
                    return Result<Operador>.Ok(operador);
                }

                operador.AddNotification(nameof(Operador), MensagensInfo.Operador_MatriculaExistente);
            }

            return Result<Operador>.Error(operador.Notifications);
        }

        #endregion
    }
}
