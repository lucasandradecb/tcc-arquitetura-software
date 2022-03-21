using Gsl.Gestao.Estrategica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Gestao.Estrategica.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de depositos
    /// </summary>
    public interface IDepositoRepository
    {
        /// <summary>
        /// Armazena um deposito no banco de dados
        /// </summary>
        /// <param name="deposito"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Deposito deposito, CancellationToken ctx);
        /// <summary>
        /// Obtém o deposito por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Deposito> ObterPorCodigo(int codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se o deposito já existe no banco
        /// </summary>
        /// <param name="deposito"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Deposito deposito, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de depositos
        /// </summary>
        /// <returns></returns>
        Task<List<Deposito>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza um deposito
        /// </summary>
        /// <returns></returns>
        /// <param name="deposito"></param>
        /// <param name="ctx"></param>
        Task Atualizar(Deposito deposito, CancellationToken ctx);
        /// <summary>
        /// Deleta o deposito por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(int codigo, CancellationToken ctx);
    }
}
