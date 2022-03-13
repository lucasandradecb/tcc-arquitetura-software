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
    /// Repositório de marca do veiculo
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MarcaVeiculoRepository : RedisRepository<MarcaVeiculo>, IMarcaVeiculoRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public MarcaVeiculoRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(MarcaVeiculo model) => ObterChave(model.Codigo);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static string ObterChave(string codigo) => $"marca:veiculo:{codigo}";

        /// <summary>
        /// Armazena a marca do veiculo no banco de dados
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(MarcaVeiculo marca, CancellationToken ctx)
        {
            await Add(marca, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém a marca pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<MarcaVeiculo> ObterPorCodigo(string codigo, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(codigo), ctx);
        }

        /// <summary>
        /// Verifica se a marcaVeiculo já existe no banco
        /// </summary>
        /// <param name="marcaVeiculo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(MarcaVeiculo marcaVeiculo, CancellationToken ctx)
        {
            var marcaExistente = await ObterPorCodigo(marcaVeiculo.Codigo, ctx);

            return marcaExistente?.Codigo == marcaVeiculo.Codigo;
        }
    }    
}