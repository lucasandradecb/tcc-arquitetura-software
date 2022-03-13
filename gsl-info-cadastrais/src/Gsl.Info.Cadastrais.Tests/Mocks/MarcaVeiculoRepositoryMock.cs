using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;

namespace Gsl.Info.Cadastrais.Tests.Mocks
{
    public class MarcaVeiculoRepositoryMock : Mock<IMarcaVeiculoRepository>
    {
        public MarcaVeiculoRepositoryMock VerificarSeExiste(bool existe)
        {
            if (!existe)
                Setup(c => c.VerificarSeExiste(It.IsAny<MarcaVeiculo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(false));
            else
                Setup(c => c.VerificarSeExiste(It.IsAny<MarcaVeiculo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

            return this;
        }

        public MarcaVeiculoRepositoryMock Salvar()
        {
            Setup(c => c.Salvar(It.IsAny<MarcaVeiculo>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return this;
        }

        public MarcaVeiculoRepositoryMock ObterPorCodigo(bool valid = true)
        {
            if (valid)
                Setup(c => c.ObterPorCodigo(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<MarcaVeiculo>(CriarMarcaVeiculo()));
            else
                Setup(c => c.ObterPorCodigo(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<MarcaVeiculo>(null));

            return this;
        }

        private MarcaVeiculo CriarMarcaVeiculo()
        {
            return new MarcaVeiculo
            {
                Codigo = "1234",
                Descricao = "Fiat",
                DataCriacao = DateTime.Now
            };
        }
    }
}
