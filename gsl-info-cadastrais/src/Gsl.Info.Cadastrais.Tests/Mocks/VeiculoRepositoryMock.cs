using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Gsl.Info.Cadastrais.Tests.Fixtures;

namespace Gsl.Info.Cadastrais.Tests.Mocks
{
    public class VeiculoRepositoryMock : Mock<IVeiculoRepository>
    {
        public VeiculoRepositoryMock VerificarSeExiste(bool existe)
        {
            if (!existe)
                Setup(c => c.VerificarSeExiste(It.IsAny<Veiculo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(false));
            else
                Setup(c => c.VerificarSeExiste(It.IsAny<Veiculo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

            return this;
        }

        public VeiculoRepositoryMock Salvar()
        {
            Setup(c => c.Salvar(It.IsAny<Veiculo>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return this;
        }

        public VeiculoRepositoryMock ObterPorId(bool valid = true)
        {
            if (valid)
                Setup(c => c.ObterPorPlaca(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Veiculo>(CriarListaVeiculos().FirstOrDefault()));
            else
                Setup(c => c.ObterPorPlaca(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Veiculo>(null));

            return this;
        }

        public VeiculoRepositoryMock ListarTodos()
        {
            Setup(c => c.ListarTodos(It.IsAny<CancellationToken>())).Returns(Task.FromResult<List<Veiculo>>(CriarListaVeiculos()));

            return this;
        }

        private List<Veiculo> CriarListaVeiculos()
        {
            return new List<Veiculo>
            {
                new VeiculoFixture().CriarVeiculo()
            };
        }
    }
}
