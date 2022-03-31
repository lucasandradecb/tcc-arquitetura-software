using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Interfaces;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using Gsl.Gestao.Estrategica.Domain.Resources;
using Flunt.Notifications;
using System.Collections.Generic;
using System;
using Gsl.Gestao.Estrategica.Infrastructure.Gateways.Interfaces;
using System.Linq;

namespace Gsl.Gestao.Estrategica.Application
{
    /// <summary>
    /// Classe de application de pedido
    /// </summary>
    public class PedidoApplication : IPedidoApplication
    {
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IGslInfoCadastraisGateway _gslInfoCadastraisGateway;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="pedidoRepository"></param>
        /// <param name="estoqueRepository"></param>
        /// <param name="gslInfoCadastraisGateway"></param>
        public PedidoApplication(IMapper mapper,
                                  IPedidoRepository pedidoRepository,
                                  IEstoqueRepository estoqueRepository,
                                  IGslInfoCadastraisGateway gslInfoCadastraisGateway)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _estoqueRepository = estoqueRepository;
            _gslInfoCadastraisGateway = gslInfoCadastraisGateway;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de pedidos 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<PedidoModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaPedidos = await _pedidoRepository.ListarTodos(ctx);

            return Result<List<PedidoModel>>.Ok(_mapper.Map<List<PedidoModel>>(listaPedidos));
        }

        /// <summary>
        /// Obtem dados de um pedido
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<PedidoModel>> ObterPedido(int codigo, CancellationToken ctx)
        {
            var output = new PedidoModel();

            var pedido = await _pedidoRepository.ObterPorCodigo(codigo, ctx);
            if (pedido == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Pedido.Codigo), MensagensInfo.Pedido_NaoEncontrado) };
                return Result<PedidoModel>.Error(notification);
            }

            output = _mapper.Map<Pedido, PedidoModel>(pedido);

            return Result<PedidoModel>.Ok(output);

        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de um pedido
        /// </summary>
        /// <param name="pedidoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Pedido>> CadastrarPedido(PedidoModel pedidoModel, CancellationToken ctx)
        {
            var pedido = _mapper.Map<PedidoModel, Pedido>(pedidoModel);

            await _pedidoRepository.Salvar(pedido, ctx);
            return Result<Pedido>.Ok(pedido);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza dados do pedido
        /// </summary>
        /// <param name="pedidoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Pedido>> AtualizarPedido(PedidoModel pedidoModel, CancellationToken ctx)
        {
            var pedido = _mapper.Map<PedidoModel, Pedido>(pedidoModel);

            if (pedido.Valid)
            {
                var pedidoAntigo = await _pedidoRepository.ObterPorCodigo(pedido.Codigo, ctx);

                if (pedidoAntigo == null)
                {
                    pedido.AddNotification(nameof(Pedido), MensagensInfo.Pedido_NaoEncontrado);
                    return Result<Pedido>.Error(pedido.Notifications);
                }

                foreach (var itemAntigo in pedidoAntigo.ItensPedido)
                {
                    var mercadoriaOutput = await _gslInfoCadastraisGateway.ObterMercadoria(itemAntigo.MercadoriaCodigo, ctx);
                    if (mercadoriaOutput != null)
                    {
                        mercadoriaOutput.Quantidade += itemAntigo.MercadoriaQuantidade;
                        await _gslInfoCadastraisGateway.AtualizarMercadoria(mercadoriaOutput, ctx);
                    }
                }

                pedido.Id = pedidoAntigo.Id;

                foreach (var item in pedido.ItensPedido)
                {
                    var mercadoriaOutput = await _gslInfoCadastraisGateway.ObterMercadoria(item.MercadoriaCodigo, ctx);
                    if (mercadoriaOutput.Invalid)
                    {
                        pedido.AddNotification(nameof(Pedido), MensagensInfo.Mercadoria_NaoEncontrada);
                        return Result<Pedido>.Error(pedido.Notifications);
                    }
                        
                    if (mercadoriaOutput.Quantidade >= item.MercadoriaQuantidade)
                    {
                        mercadoriaOutput.Quantidade -= item.MercadoriaQuantidade;
                        await _gslInfoCadastraisGateway.AtualizarMercadoria(mercadoriaOutput, ctx);
                    }
                    else
                    {
                        var erroMsg = string.Format(MensagensInfo.Mercadoria_MaxQuantidade, mercadoriaOutput.Nome, mercadoriaOutput.Quantidade);
                        pedido.AddNotification(nameof(Pedido), erroMsg);
                    }

                    item.Valor = mercadoriaOutput.Valor * Convert.ToDouble(item.MercadoriaQuantidade);
                }
            }

            pedido.ValorTotal = pedido.ItensPedido.Sum(x => x.Valor);

            if (pedido.Invalid)
                return Result<Pedido>.Error(pedido.Notifications);

            await _pedidoRepository.Atualizar(pedido, ctx);
            return Result<Pedido>.Ok(pedido);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta um Pedido pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Pedido>> DeletarPedido(int codigo, CancellationToken ctx)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorCodigo(codigo, ctx);
                await _pedidoRepository.Deletar(pedido, ctx);
            }
            catch (Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(Pedido.Codigo), MensagensInfo.Pedido_ErroDeletar) };
                return Result<Pedido>.Error(notification);
            }

            return Result<Pedido>.Ok(new Pedido());
        }

        #endregion

    }
}
