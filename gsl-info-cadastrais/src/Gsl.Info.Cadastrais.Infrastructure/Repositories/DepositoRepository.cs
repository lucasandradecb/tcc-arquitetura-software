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
    /// Repositório de depositos
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DepositoRepository : RedisRepository<Deposito>, IDepositoRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public DepositoRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Deposito model) => ObterChave(model.Codigo.ToString());

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static string ObterChave(string codigo) => $"deposito:{codigo}";

        /// <summary>
        /// Armazena o deposito no banco de dados
        /// </summary>
        /// <param name="deposito"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Deposito deposito, CancellationToken ctx)
        {
            await Add(deposito, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o deposito pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Deposito> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(codigo.ToString()), ctx);
        }

        /// <summary>
        /// Verifica se o deposito já existe no banco
        /// </summary>
        /// <param name="deposito"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Deposito deposito, CancellationToken ctx)
        {
            var depositoExistente = await ObterPorCodigo(deposito.Codigo, ctx);

            return depositoExistente?.Codigo == deposito.Codigo;
        }
        
        /// <summary>
        /// Obtém lista de depositos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Deposito>> ListarTodos(CancellationToken ctx)
        {
            var listaChaves = GetServer().Keys(pattern: "deposito:*").ToList();
            var listaDepositos = new List<Deposito>();

            foreach (var chave in listaChaves)
            {
                var deposito = await GetByKey(chave, ctx);
                if (deposito != default)
                    listaDepositos.Add(deposito);
            }

            return listaDepositos;
        }

        public Task Atualizar(Deposito deposito, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }
    }    
}