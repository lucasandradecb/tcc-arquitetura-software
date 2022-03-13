using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;

namespace Gsl.Info.Cadastrais.Application.Interfaces
{
    /// <summary>
    /// Interface de UsuarioApplication
    /// </summary>
    public interface IUsuarioApplication
    {
        /// <summary>
        /// Realiza o cadastro de um usuario
        /// </summary>
        /// <param name="usuarioModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<UsuarioModel>> CadastrarUsuario(UsuarioModel usuarioModel, CancellationToken ctx);

        /// <summary>
        /// Obtem dados de um usuario
        /// </summary>
        /// <param name="usuarioModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<UsuarioTipoModel>> ObterUsuario(UsuarioModel usuarioModel, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Cliente>> CadastrarCliente(ClienteModel clienteModel, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de um operador
        /// </summary>
        /// <param name="operadorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Operador>> CadastrarOperador(OperadorModel operadorModel, CancellationToken ctx);
    }
}