using Gsl.Info.Cadastrais.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de marca veiculos
    /// </summary>
    public interface IMarcaVeiculoRepository
    {
        /// <summary>
        /// Armazena a marca de um veiculo no banco de dados
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(MarcaVeiculo marca, CancellationToken ctx);
        /// <summary>
        /// Obtém a marca do veiculo por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<MarcaVeiculo> ObterPorCodigo(string codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se a marcaVeiculo já existe no banco
        /// </summary>
        /// <param name="marcaVeiculo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(MarcaVeiculo marcaVeiculo, CancellationToken ctx);
    }
}
