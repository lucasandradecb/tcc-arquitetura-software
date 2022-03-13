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
    /// Repositório de agendamentos
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AgendamentoRepository : RedisRepository<Agendamento>, IAgendamentoRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public AgendamentoRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Agendamento model) => ObterChave(model.CodigoReserva);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="codigoReserva"></param>
        /// <returns></returns>
        public static string ObterChave(string codigoReserva) => $"agendamento:{codigoReserva}";

        /// <summary>
        /// Armazena o agendamento no banco de dados
        /// </summary>
        /// <param name="agendamento"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Agendamento agendamento, CancellationToken ctx)
        {
            await Add(agendamento, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o agendamento pelo codigo da reserva
        /// </summary>
        /// <param name="codigoReserva"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Agendamento> ObterPorReserva(string codigoReserva, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(codigoReserva), ctx); 
        }

        /// <summary>
        /// Verifica se o agendamento já existe no banco
        /// </summary>
        /// <param name="agendamento"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Agendamento agendamento, CancellationToken ctx)
        {
            var agendamentoExistente = await ObterPorReserva(agendamento.CodigoReserva, ctx);

            return agendamentoExistente?.CodigoReserva == agendamento.CodigoReserva;
        }

    }    
}