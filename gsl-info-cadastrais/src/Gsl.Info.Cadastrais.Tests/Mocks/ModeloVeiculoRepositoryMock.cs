using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;

namespace Gsl.Info.Cadastrais.Tests.Mocks
{
    public class ModeloVeiculoRepositoryMock : Mock<IModeloVeiculoRepository>
    {
        public ModeloVeiculoRepositoryMock VerificarSeExiste(bool existe)
        {
            if (!existe)
                Setup(c => c.VerificarSeExiste(It.IsAny<ModeloVeiculo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(false));
            else
                Setup(c => c.VerificarSeExiste(It.IsAny<ModeloVeiculo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

            return this;
        }

        public ModeloVeiculoRepositoryMock Salvar()
        {
            Setup(c => c.Salvar(It.IsAny<ModeloVeiculo>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return this;
        }

        public ModeloVeiculoRepositoryMock ObterPorCodigo(bool valid = true)
        {
            if (valid)
                Setup(c => c.ObterPorCodigo(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<ModeloVeiculo>(CriarModeloVeiculo()));
            else
                Setup(c => c.ObterPorCodigo(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<ModeloVeiculo>(null));

            return this;
        }

        private ModeloVeiculo CriarModeloVeiculo()
        {
            return new ModeloVeiculo
            {
                Codigo = "1234",
                Descricao = "Palio 1.0",
                DataCriacao = DateTime.Now
            };
        }
    }
}
