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
    /// Repositório de mercadorias
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MercadoriaRepository : RedisRepository<Mercadoria>, IMercadoriaRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public MercadoriaRepository(RedisConfiguration config) : base(config)
        {

        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Mercadoria model) => ObterChave(model.Codigo.ToString());

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static string ObterChave(string codigo) => $"mercadoria:{codigo}";

        /// <summary>
        /// Armazena a mercadoria no banco de dados
        /// </summary>
        /// <param name="mercadoria"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Mercadoria mercadoria, CancellationToken ctx)
        {
            await Add(mercadoria, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém a mercadoria pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Mercadoria> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(codigo.ToString()), ctx);
        }

        /// <summary>
        /// Verifica se a mercadoria já existe no banco
        /// </summary>
        /// <param name="mercadoria"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Mercadoria mercadoria, CancellationToken ctx)
        {
            var mercadoriaExistente = await ObterPorCodigo(mercadoria.Codigo, ctx);

            return mercadoriaExistente?.Codigo == mercadoria.Codigo;
        }

        /// <summary>
        /// Obtém lista de mercadorias
        /// </summary>
        /// <returns></returns>
        public async Task<List<Mercadoria>> ListarTodos(CancellationToken ctx)
        {
            var listaChaves = GetServer().Keys(pattern: "mercadoria:*").ToList();
            var listaMercadorias = new List<Mercadoria>();

            foreach (var chave in listaChaves)
            {
                var mercadoria = await GetByKey(chave, ctx);
                if (mercadoria != default)
                    listaMercadorias.Add(mercadoria);
            }

            return listaMercadorias;
        }
    }    
}