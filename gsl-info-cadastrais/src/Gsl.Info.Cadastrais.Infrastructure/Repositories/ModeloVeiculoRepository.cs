using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using Localiza.BuildingBlocks.Redis;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics.CodeAnalysis;

namespace Gsl.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de modelo do veiculo
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ModeloVeiculoRepository : RedisRepository<ModeloVeiculo>, IModeloVeiculoRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public ModeloVeiculoRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(ModeloVeiculo model) => ObterChave(model.Codigo);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static string ObterChave(string codigo) => $"modelo:veiculo:{codigo}";

        /// <summary>
        /// Armazena o modelo do veiculo no banco de dados
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(ModeloVeiculo modelo, CancellationToken ctx)
        {
            await Add(modelo, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o modelo pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<ModeloVeiculo> ObterPorCodigo(string codigo, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(codigo), ctx);
        }

        /// <summary>
        /// Verifica se a modeloVeiculo já existe no banco
        /// </summary>
        /// <param name="modeloVeiculo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(ModeloVeiculo modeloVeiculo, CancellationToken ctx)
        {
            var modeloExistente = await ObterPorCodigo(modeloVeiculo.Codigo, ctx);

            return modeloExistente?.Codigo == modeloVeiculo.Codigo;
        }
    }    
}