using Gsl.Gestao.Estrategica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Gestao.Estrategica.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de fornecedores
    /// </summary>
    public interface IFornecedorRepository
    {
        /// <summary>
        /// Armazena um fornecedor no banco de dados
        /// </summary>
        /// <param name="fornecedor"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Fornecedor fornecedor, CancellationToken ctx);
        /// <summary>
        /// Obtém o fornecedor por cnpj
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Fornecedor> ObterPorCnpj(string cnpj, CancellationToken ctx);
        /// <summary>
        /// Verifica se o fornecedor já existe no banco
        /// </summary>
        /// <param name="fornecedor"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Fornecedor fornecedor, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de fornecedores
        /// </summary>
        /// <returns></returns>
        Task<List<Fornecedor>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza um fornecedor
        /// </summary>
        /// <returns></returns>
        /// <param name="fornecedor"></param>
        /// <param name="ctx"></param>
        Task Atualizar(Fornecedor fornecedor, CancellationToken ctx);
        /// <summary>
        /// Deleta o fornecedor pelo cnpj
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Deletar(string cnpj, CancellationToken ctx);
    }
}
