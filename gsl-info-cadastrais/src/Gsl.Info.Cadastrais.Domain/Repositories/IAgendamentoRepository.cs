using Gsl.Info.Cadastrais.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Info.Cadastrais.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de agendamentos
    /// </summary>
    public interface IAgendamentoRepository
    {
        /// <summary>
        /// Armazena um agendamento no banco de dados
        /// </summary>
        /// <param name="agendamento"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task Salvar(Agendamento agendamento, CancellationToken ctx);
        /// <summary>
        /// Obtém o agendamento por codigo da reserva
        /// </summary>
        /// <param name="codigoReserva"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Agendamento> ObterPorReserva(string codigoReserva, CancellationToken ctx);
        /// <summary>
        /// Verifica se o agendamento já existe no banco
        /// </summary>
        /// <param name="agendamento"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<bool> VerificarSeExiste(Agendamento agendamento, CancellationToken ctx);
    }
}
