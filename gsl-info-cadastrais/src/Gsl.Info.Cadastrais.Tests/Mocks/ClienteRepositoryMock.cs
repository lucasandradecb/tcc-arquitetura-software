using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;
using Gsl.Info.Cadastrais.Tests.Fixtures;

namespace Gsl.Info.Cadastrais.Tests.Mocks
{
    public class ClienteRepositoryMock : Mock<IClienteRepository>
    {
        public ClienteRepositoryMock VerificarSeExiste(bool existe = true)
        {
            if (!existe)
                Setup(c => c.VerificarSeExiste(It.IsAny<Cliente>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(false));
            else
                Setup(c => c.VerificarSeExiste(It.IsAny<Cliente>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

            return this;
        }

        public ClienteRepositoryMock Salvar()
        {
            Setup(c => c.Salvar(It.IsAny<Cliente>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return this;
        }

        public ClienteRepositoryMock ObterPorCpf(bool valid = true)
        {
            if (valid)
                Setup(c => c.ObterPorCpf(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Cliente>(CriarCliente()));
            else
                Setup(c => c.ObterPorCpf(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Cliente>(null));

            return this;
        }

        private Cliente CriarCliente()
        {
            return new Cliente
            {
                Aniversario = DateTime.Now.AddYears(-20),
                Cpf = new CPF("90459735020"),
                DataCriacao = DateTime.Now,
                Nome = "Teste",
                Endereco = new EnderecoFixture().CriarEndereco()
            };
        }
    }
}
