using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;

namespace Gsl.Gestao.Estrategica.Application.Interfaces
{
    /// <summary>
    /// Interface de EntregaApplication
    /// </summary>
    public interface IEntregaApplication
    {
        /// <summary>
        /// Obtém a lista de todos os entregas
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<EntregaModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de uma entrega
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<EntregaModel>> ObterEntrega(string codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de uma entrega
        /// </summary>
        /// <param name="entregaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Entrega>> CadastrarEntrega(EntregaModel entregaModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de uma entrega
        /// </summary>
        /// <param name="entregaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Entrega>> AtualizarEntrega(EntregaModel entregaModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um entrega
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Entrega>> DeletarEntrega(string codigo, CancellationToken ctx);
    }
}