using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;

namespace Gsl.Gestao.Estrategica.Application.Interfaces
{
    /// <summary>
    /// Interface de PedidoApplication
    /// </summary>
    public interface IPedidoApplication
    {
        /// <summary>
        /// Obtém a lista de todos os pedidos
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<PedidoModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de um pedido
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<PedidoModel>> ObterPedido(int codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro um de pedido
        /// </summary>
        /// <param name="pedidoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Pedido>> CadastrarPedido(PedidoModel pedidoModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de pedido
        /// </summary>
        /// <param name="pedidoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Pedido>> AtualizarPedido(PedidoModel pedidoModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um pedido
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Pedido>> DeletarPedido(int codigo, CancellationToken ctx);
    }
}