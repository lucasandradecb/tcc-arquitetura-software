using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using Moq;

namespace Gsl.Info.Cadastrais.Tests.Mocks
{
    public class UsuarioRepositoryMock : Mock<IUsuarioRepository>
    {
        public UsuarioRepositoryMock VerificarSeExiste(bool existe)
        {
            if (!existe)
                Setup(c => c.VerificarSeExiste(It.IsAny<Usuario>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(false));
            else
                Setup(c => c.VerificarSeExiste(It.IsAny<Usuario>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

            return this;
        }

        public UsuarioRepositoryMock Salvar()
        {
            Setup(c => c.Salvar(It.IsAny<Usuario>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            return this;
        }

        public UsuarioRepositoryMock ObterPorLogin(bool valid = true)
        {
            if (valid)
                Setup(c => c.ObterPorLogin(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Usuario>(CriarUsuario()));
            else
                Setup(c => c.ObterPorLogin(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Usuario>(null));

            return this;
        }

        private Usuario CriarUsuario()
        {
            return new Usuario
            {                
                Login = "90459735020",
                DataCriacao = DateTime.Now,
                Senha = "ce0bfd15059b68d67688884d7a3d3e8c"
            };
        }
    }
}
