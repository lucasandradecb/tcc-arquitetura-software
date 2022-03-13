using Gsl.Info.Cadastrais.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de usuario
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Armazena um usuario no banco de dados
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Usuario usuario, CancellationToken ctx);
        /// <summary>
        /// Obtém o usuario por login
        /// </summary>
        /// <param name="login"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Usuario> ObterPorLogin(string login, CancellationToken ctx);
        /// <summary>
        /// Verifica se o usuario já existe no banco
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Usuario usuario, CancellationToken ctx);
    }
}
