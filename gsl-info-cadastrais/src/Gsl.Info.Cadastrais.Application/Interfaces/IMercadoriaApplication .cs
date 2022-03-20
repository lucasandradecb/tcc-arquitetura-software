using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;

namespace Gsl.Info.Cadastrais.Application.Interfaces
{
    /// <summary>
    /// Interface de MercadoriaApplication 
    /// </summary>
    public interface IMercadoriaApplication
    {
        /// <summary>
        /// Obtém a lista de todos as mercadorias
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<List<MercadoriaModel>>> ListarTodos(CancellationToken ctx);

        /// <summary>
        /// Obtem dados de uma mercadoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<MercadoriaModel>> ObterMercadoria(int codigo, CancellationToken ctx);

        /// <summary>
        /// Realiza o cadastro de uma Mercadoria
        /// </summary>
        /// <param name="mercadoriaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Mercadoria>> CadastrarMercadoria(MercadoriaModel mercadoriaModel, CancellationToken ctx);

        /// <summary>
        /// Realiza a atualização de uma Mercadoria
        /// </summary>
        /// <param name="mercadoriaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Mercadoria>> AtualizarMercadoria(MercadoriaModel mercadoriaModel, CancellationToken ctx);

        /// <summary>
        /// Deletar de uma mercadoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<Mercadoria>> DeletarMercadoria(int codigo, CancellationToken ctx);
    }
}