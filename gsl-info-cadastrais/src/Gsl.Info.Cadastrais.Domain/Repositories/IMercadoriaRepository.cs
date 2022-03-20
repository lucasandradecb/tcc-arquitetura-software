using Gsl.Info.Cadastrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de mercadorias
    /// </summary>
    public interface IMercadoriaRepository
    {
        /// <summary>
        /// Armazena uma mercadoria no banco de dados
        /// </summary>
        /// <param name="mercadoria"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Mercadoria mercadoria, CancellationToken ctx);
        /// <summary>
        /// Obtém a mercadoria por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Mercadoria> ObterPorCodigo(int codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se a mercadoria já existe no banco
        /// </summary>
        /// <param name="mercadoria"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Mercadoria mercadoria, CancellationToken ctx);
        /// <summary>
        /// Obtém lista de mercadorias
        /// </summary>
        /// <returns></returns>
        Task<List<Mercadoria>> ListarTodos(CancellationToken ctx);
        /// <summary>
        /// Atualiza uma mercadoria
        /// </summary>
        /// <returns></returns>
        /// <param name="mercadoria"></param>
        /// <param name="ctx"></param>
        Task<Mercadoria> Atualizar(Mercadoria mercadoria, CancellationToken ctx);
        /// <summary>
        /// Deleta a mercadoria por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Mercadoria> Deletar(int codigo, CancellationToken ctx);
    }
}
