using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Interfaces;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using Gsl.Gestao.Estrategica.Domain.Resources;
using Gsl.Gestao.Estrategica.Domain.ValueObjects;
using Flunt.Notifications;
using System.Collections.Generic;
using System;
using Gsl.Gestao.Estrategica.Domain.Enums;

namespace Gsl.Gestao.Estrategica.Application
{
    /// <summary>
    /// Classe de application de entrega
    /// </summary>
    public class EntregaApplication : IEntregaApplication
    {
        private readonly IMapper _mapper;
        private readonly IEntregaRepository _entregaRepository;
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="entregaRepository"></param>
        /// <param name="pedidoRepository"></param>
        public EntregaApplication(IMapper mapper, 
                                  IEntregaRepository entregaRepository,
                                  IPedidoRepository pedidoRepository)
        {
            _mapper = mapper;
            _entregaRepository = entregaRepository;
            _pedidoRepository = pedidoRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de entregas 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<EntregaModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaEntregas = await _entregaRepository.ListarTodos(ctx);

            return Result<List<EntregaModel>>.Ok(_mapper.Map<List<EntregaModel>>(listaEntregas));
        }

        /// <summary>
        /// Obtem dados de uma entrega
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<EntregaModel>> ObterEntrega(string codigo, CancellationToken ctx)
        {
            var output = new EntregaModel();

            var entrega = await _entregaRepository.ObterPorCodigo(codigo, ctx);
            if (entrega == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Entrega.Codigo), MensagensInfo.Entrega_NaoEncontrada) };
                return Result<EntregaModel>.Error(notification);
            }

            output = _mapper.Map<Entrega, EntregaModel>(entrega);

            return Result<EntregaModel>.Ok(output);

        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de uma entrega
        /// </summary>
        /// <param name="entregaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Entrega>> CadastrarEntrega(EntregaModel entregaModel, CancellationToken ctx)
        {
            var entrega = _mapper.Map<EntregaModel, Entrega>(entregaModel);

            if (entrega.Valid)
            {
                var pedido = await _pedidoRepository.ObterPorCodigo(entregaModel.PedidoCodigo, ctx);
                entrega.PedidoId = pedido.Id;
                entrega.StatusEntrega = EStatusEntrega.EmAndamento;

                if (!await _entregaRepository.VerificarSeExiste(entrega, ctx))
                {
                    await _entregaRepository.Salvar(entrega, ctx);
                    return Result<Entrega>.Ok(entrega);
                }

                entrega.AddNotification(nameof(Entrega.Codigo), MensagensInfo.Entrega_CodigoExistente);
            }

            return Result<Entrega>.Error(entrega.Notifications);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza dados da entrega
        /// </summary>
        /// <param name="entregaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Entrega>> AtualizarEntrega(EntregaModel entregaModel, CancellationToken ctx)
        {
            var entrega = _mapper.Map<EntregaModel, Entrega>(entregaModel);

            if (entrega.Valid)
            {
                if (await _entregaRepository.VerificarSeExiste(entrega, ctx))
                {
                    await _entregaRepository.Atualizar(entrega, ctx);
                    return Result<Entrega>.Ok(entrega);
                }

                entrega.AddNotification(nameof(Entrega.Codigo), MensagensInfo.Entrega_NaoEncontrada);
            }

            return Result<Entrega>.Error(entrega.Notifications);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta uma entrega pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Entrega>> DeletarEntrega(string codigo, CancellationToken ctx)
        {
            try
            {
                await _entregaRepository.Deletar(codigo, ctx);
            }
            catch (Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(Entrega.Codigo), MensagensInfo.Entrega_ErroDeletar) };
                return Result<Entrega>.Error(notification);
            }

            return Result<Entrega>.Ok(new Entrega());
        }

        #endregion

    }
}
