using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;
using Gsl.Info.Cadastrais.Domain.Enums;

namespace Gsl.Info.Cadastrais.Tests.Mocks
{
    public class AgendamentoRepositoryMock : Mock<IAgendamentoRepository>
    {
        public AgendamentoRepositoryMock VerificarSeExiste(bool existe)
        {
            if (!existe)
                Setup(c => c.VerificarSeExiste(It.IsAny<Agendamento>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(false));
            else
                Setup(c => c.VerificarSeExiste(It.IsAny<Agendamento>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

            return this;
        }

        public AgendamentoRepositoryMock Salvar()
        {
            Setup(c => c.Salvar(It.IsAny<Agendamento>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return this;
        }

        public AgendamentoRepositoryMock ObterPorReserva(bool valid = true)
        {
            if (valid)
                Setup(c => c.ObterPorReserva(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Agendamento>(CriarAgendamento()));
            else
                Setup(c => c.ObterPorReserva(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Agendamento>(null));

            return this;
        }

        private Agendamento CriarAgendamento()
        {
            return new Agendamento
            {
                CategoriaVeiculo = ECategoriaVeiculo.Basico,
                CodigoReserva = "6MQEW4UEH2",
                Placa = "AAA1234",
                TotalHoras = 48,
                ValorHoraVeiculo = 10,
                ValorTotalAluguel = 480,
                DataCriacao = DateTime.Now
            };
        }
    }
}
