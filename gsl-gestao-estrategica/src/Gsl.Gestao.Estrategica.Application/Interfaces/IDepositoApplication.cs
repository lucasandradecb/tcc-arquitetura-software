using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;

namespace Gsl.Gestao.Estrategica.Application.Interfaces
{
    /// <summary>
    /// Interface de DepositoApplication
    /// </summary>
    public interface IDepositoApplication
    {
        /// <summary>
        /// Obtém a lista de todos os depositos
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<DepositoModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de um deposito
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<DepositoModel>> ObterDeposito(int codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de um deposito
        /// </summary>
        /// <param name="depositoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Deposito>> CadastrarDeposito(DepositoModel depositoModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de um deposito
        /// </summary>
        /// <param name="depositoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Deposito>> AtualizarDeposito(DepositoModel depositoModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um deposito
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Deposito>> DeletarDeposito(int codigo, CancellationToken ctx);
    }
}