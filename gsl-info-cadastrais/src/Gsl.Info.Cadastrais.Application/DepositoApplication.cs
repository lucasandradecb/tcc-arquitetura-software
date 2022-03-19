using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Domain.Resources;
using Flunt.Notifications;
using System.Collections.Generic;

namespace Gsl.Info.Cadastrais.Application
{
    /// <summary>
    /// Classe de application de deposito
    /// </summary>
    public class DepositoApplication : IDepositoApplication
    {
        private readonly IMapper _mapper;
        private readonly IDepositoRepository _depositoRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="depositoRepository"></param>
        public DepositoApplication(IMapper mapper,
                                     IDepositoRepository depositoRepository)
        {
            _mapper = mapper;
            _depositoRepository = depositoRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de depositos 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<DepositoModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaDepositos = await _depositoRepository.ListarTodos(ctx);

            return Result<List<DepositoModel>>.Ok(_mapper.Map<List<DepositoModel>>(listaDepositos));
        }

        /// <summary>
        /// Obtem dados de um deposito
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<DepositoModel>> ObterDeposito(int codigo, CancellationToken ctx)
        {
            var output = new DepositoModel();

            var deposito = await _depositoRepository.ObterPorCodigo(codigo, ctx);
            if (deposito == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Deposito.Codigo), MensagensInfo.Deposito_NaoEncontrado) };
                return Result<DepositoModel>.Error(notification);
            }

            output = _mapper.Map<Deposito, DepositoModel>(deposito);            

            return Result<DepositoModel>.Ok(output);
        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de um deposito
        /// </summary>
        /// <param name="depositoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Deposito>> CadastrarDeposito(DepositoModel depositoModel, CancellationToken ctx)
        {
            var deposito = _mapper.Map<DepositoModel, Deposito>(depositoModel);

            if (deposito.Valid)
            {
                if (!await _depositoRepository.VerificarSeExiste(deposito, ctx))
                {
                    await _depositoRepository.Salvar(deposito, ctx);
                    return Result<Deposito>.Ok(deposito);
                }

                deposito.AddNotification(nameof(Deposito.Codigo), MensagensInfo.Deposito_CodigoExiste);
            }

            return Result<Deposito>.Error(deposito.Notifications);
        }

        #endregion

    }
}
