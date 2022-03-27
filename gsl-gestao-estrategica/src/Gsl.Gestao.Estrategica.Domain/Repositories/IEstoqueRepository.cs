using Gsl.Gestao.Estrategica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Gestao.Estrategica.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de estoque
    /// </summary>
    public interface IEstoqueRepository
    {
        /// <summary>
        /// Armazena um estoque no banco de dados
        /// </summary>
        /// <param name="estoque"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Estoque estoque, CancellationToken ctx);
        /// <summary>
        /// Obtém o estoque por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Estoque> ObterPorCodigo(int codigo, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de estoques
        /// </summary>
        /// <returns></returns>
        Task<List<Estoque>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza um estoque
        /// </summary>
        /// <returns></returns>
        /// <param name="estoque"></param>
        /// <param name="ctx"></param>
        Task Atualizar(Estoque estoque, CancellationToken ctx);
        /// <summary>
        /// Deleta o estoque pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(int codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se o estoque já existe no banco
        /// </summary>
        /// <param name="estoque"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Estoque estoque, CancellationToken ctx);
    }
}
