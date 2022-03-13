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
    /// Repositório de operadores
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class OperadorRepository : RedisRepository<Operador>, IOperadorRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public OperadorRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Operador model) => ObterChave(model.Matricula);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        public static string ObterChave(string matricula) => $"operador:{matricula}";

        /// <summary>
        /// Armazena o operador no banco de dados
        /// </summary>
        /// <param name="operador"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Operador operador, CancellationToken ctx)
        {
            await Add(operador, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o operador pela matricula
        /// </summary>
        /// <param name="matricula"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Operador> ObterPorMatricula(string matricula, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(matricula), ctx);
        }

        /// <summary>
        /// Verifica se o operador já existe no banco
        /// </summary>
        /// <param name="operador"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Operador operador, CancellationToken ctx)
        {
            var operadorExistente = await ObterPorMatricula(operador.Matricula, ctx);

            return operadorExistente?.Matricula == operador.Matricula;
        }
    }    
}