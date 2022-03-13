using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;

namespace Gsl.Info.Cadastrais.Tests.Mocks
{
    public class OperadorRepositoryMock : Mock<IOperadorRepository>
    {
        public OperadorRepositoryMock VerificarSeExiste(bool existe)
        {
            if (!existe)
                Setup(c => c.VerificarSeExiste(It.IsAny<Operador>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(false));
            else
                Setup(c => c.VerificarSeExiste(It.IsAny<Operador>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

            return this;
        }

        public OperadorRepositoryMock Salvar()
        {
            Setup(c => c.Salvar(It.IsAny<Operador>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return this;
        }

        public OperadorRepositoryMock ObterPorMatricula(bool valid = true)
        {
            if (valid)
                Setup(c => c.ObterPorMatricula(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Operador>(CriarOperador()));
            else
                Setup(c => c.ObterPorMatricula(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Operador>(null));

            return this;
        }

        private Operador CriarOperador()
        {
            return new Operador
            {
                DataCriacao = DateTime.Now,
                Matricula = "123456",
                Nome = "Teste"
            };
        }
    }
}
