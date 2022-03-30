using Gsl.Gestao.Estrategica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Gestao.Estrategica.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de pedido
    /// </summary>
    public interface IPedidoRepository
    {
        /// <summary>
        /// Armazena um pedido no banco de dados
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Pedido pedido, CancellationToken ctx);
        /// <summary>
        /// Obtém o pedido por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Pedido> ObterPorCodigo(int codigo, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de pedidos
        /// </summary>
        /// <returns></returns>
        Task<List<Pedido>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza um pedidos
        /// </summary>
        /// <returns></returns>
        /// <param name="pedido"></param>
        /// <param name="ctx"></param>
        Task Atualizar(Pedido pedido, CancellationToken ctx);
        /// <summary>
        /// Deleta o pedido 
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(Pedido pedido, CancellationToken ctx);
        /// <summary>
        /// Verifica se o pedido já existe no banco
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Pedido pedido, CancellationToken ctx);
        // <summary>
        /// Armazena um itemPedido no banco de dados
        /// </summary>
        /// <param name="itemPedido"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task SalvarItem(ItemPedido itemPedido, CancellationToken ctx);
        /// <summary>
        /// Deleta item do pedido
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task DeletarItem(int codigo, CancellationToken ctx);
    }
}
