using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;

namespace Gsl.Info.Cadastrais.Application.Interfaces
{
    /// <summary>
    /// Interface de AgendamentoApplication
    /// </summary>
    public interface IAgendamentoApplication
    {
        /// <summary>
        /// Realiza a simulação de um agendamento
        /// </summary>
        /// <param name="agendamentoInput"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<SimulacaoAgendamentoModel>> SimularAluguelVeiculo(AgendamentoInputModel agendamentoInput, CancellationToken ctx);

        /// <summary>
        /// Realiza o agendamento de um aluguel de veiculo
        /// </summary>
        /// <param name="agendamentoInput"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<Result<AgendamentoOutputModel>> AgendarAluguelVeiculo(AgendamentoInputModel agendamentoInput, CancellationToken ctx);
    }
}