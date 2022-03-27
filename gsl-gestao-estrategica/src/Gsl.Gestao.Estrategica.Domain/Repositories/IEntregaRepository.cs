using Gsl.Gestao.Estrategica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Gestao.Estrategica.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de entregas
    /// </summary>
    public interface IEntregaRepository
    {
        /// <summary>
        /// Armazena um entrega no banco de dados
        /// </summary>
        /// <param name="entrega"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Entrega entrega, CancellationToken ctx);
        /// <summary>
        /// Obtém o entrega por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Entrega> ObterPorCodigo(string codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se o entrega já existe no banco
        /// </summary>
        /// <param name="entrega"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Entrega entrega, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de entregas
        /// </summary>
        /// <returns></returns>
        Task<List<Entrega>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza uma entrega
        /// </summary>
        /// <returns></returns>
        /// <param name="entrega"></param>
        /// <param name="ctx"></param>
        Task Atualizar(Entrega entrega, CancellationToken ctx);
        /// <summary>
        /// Deleta a entrega pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(string codigo, CancellationToken ctx);
    }
}
