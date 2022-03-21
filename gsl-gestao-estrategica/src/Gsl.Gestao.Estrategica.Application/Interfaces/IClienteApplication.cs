using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;

namespace Gsl.Gestao.Estrategica.Application.Interfaces
{
    /// <summary>
    /// Interface de ClienteApplication
    /// </summary>
    public interface IClienteApplication
    {
        /// <summary>
        /// Obtém a lista de todos os clientes
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<ClienteModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de um cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<ClienteModel>> ObterCliente(string cpf, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Cliente>> CadastrarCliente(ClienteModel clienteModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Cliente>> AtualizarCliente(ClienteModel clienteModel, CancellationToken ctx);

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Cliente>> DeletarCliente(string cpf, CancellationToken ctx);
    }
}