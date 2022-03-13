using Gsl.Info.Cadastrais.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de modelo de veiculos
    /// </summary>
    public interface IModeloVeiculoRepository
    {
        /// <summary>
        /// Armazena o modelo de um veiculo no banco de dados
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(ModeloVeiculo marca, CancellationToken ctx);
        /// <summary>
        /// Obtém o modelo do veiculo por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<ModeloVeiculo> ObterPorCodigo(string codigo, CancellationToken ctx);
        /// <summary>
        /// Verifica se o modelo já existe no banco
        /// </summary>
        /// <param name="modeloVeiculo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(ModeloVeiculo modeloVeiculo, CancellationToken ctx);
    }
}
