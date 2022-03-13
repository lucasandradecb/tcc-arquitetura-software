using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using Localiza.BuildingBlocks.Redis;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Gsl.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de veiculos
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VeiculoRepository : RedisRepository<Veiculo>, IVeiculoRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public VeiculoRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Veiculo model) => ObterChave(model.Placa);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="placa"></param>
        /// <returns></returns>
        public static string ObterChave(string placa) => $"veiculo:{placa}";

        /// <summary>
        /// Armazena o veiculo no banco de dados
        /// </summary>
        /// <param name="veiculo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Veiculo veiculo, CancellationToken ctx)
        {
            await Add(veiculo, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o veiculo pela placa
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Veiculo> ObterPorPlaca(string placa, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(placa), ctx); 
        }

        /// <summary>
        /// Verifica se o veiculo já existe no banco
        /// </summary>
        /// <param name="veiculo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Veiculo veiculo, CancellationToken ctx)
        {
            var veiculoExistente = await ObterPorPlaca(veiculo.Placa, ctx);

            return veiculoExistente?.Placa == veiculo.Placa;
        }

        /// <summary>
        /// Obtém lista de veiculos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Veiculo>> ListarTodos(CancellationToken ctx)
        {
            var listaChaves = GetServer().Keys(pattern: "veiculo:*").ToList();
            var listaVeiculos = new List<Veiculo>();
            
            foreach (var chave in listaChaves)
            {
                var veiculo = await GetByKey(chave, ctx);
                if (veiculo != default)
                    listaVeiculos.Add(veiculo);
            }

            return listaVeiculos;
        }

    }    
}