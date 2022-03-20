using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;

namespace Gsl.Info.Cadastrais.Application.Interfaces
{
    /// <summary>
    /// Interface de FornecedorApplication
    /// </summary>
    public interface IFornecedorApplication
    {
        /// <summary>
        /// Obtém a lista de todos os fornecedores
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<FornecedorModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de um fornecedor
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<FornecedorModel>> ObterFornecedor(string cnpj, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de um fornecedor
        /// </summary>
        /// <param name="fornecedorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Fornecedor>> CadastrarFornecedor(FornecedorModel fornecedorModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de um fornecedor
        /// </summary>
        /// <param name="fornecedorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Fornecedor>> AtualizarFornecedor (FornecedorModel fornecedorModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um fornecedor
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Fornecedor>> DeletarFornecedor(string cnpj, CancellationToken ctx);
    }
}