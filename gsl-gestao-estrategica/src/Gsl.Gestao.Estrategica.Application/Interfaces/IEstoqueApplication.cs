using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;

namespace Gsl.Gestao.Estrategica.Application.Interfaces
{
    /// <summary>
    /// Interface de EstoqueApplication
    /// </summary>
    public interface IEstoqueApplication
    {
        /// <summary>
        /// Obtém a lista de todos os estoques
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<EstoqueModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de um estoque
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<EstoqueModel>> ObterEstoque(int codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de um estoque
        /// </summary>
        /// <param name="estoqueModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Estoque>> CadastrarEstoque(EstoqueModel estoqueModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de um estoque
        /// </summary>
        /// <param name="estoqueModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Estoque>> AtualizarEstoque(EstoqueModel estoqueModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um estoque
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Estoque>> DeletarEstoque(int codigo, CancellationToken ctx);
    }
}