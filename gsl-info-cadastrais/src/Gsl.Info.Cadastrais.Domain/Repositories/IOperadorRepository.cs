using Gsl.Info.Cadastrais.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de operadores
    /// </summary>
    public interface IOperadorRepository
    {
        /// <summary>
        /// Armazena um operador no banco de dados
        /// </summary>
        /// <param name="operador"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Operador operador, CancellationToken ctx);
        /// <summary>
        /// Obtém o operador por matricula
        /// </summary>
        /// <param name="matricula"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Operador> ObterPorMatricula(string matricula, CancellationToken ctx);
        /// <summary>
        /// Verifica se o operador já existe no banco
        /// </summary>
        /// <param name="operador"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Operador operador, CancellationToken ctx);
    }
}
