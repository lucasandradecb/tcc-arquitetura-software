using Microsoft.AspNetCore.Mvc;
using Gsl.Info.Cadastrais.Api.Controllers.v1;
using Gsl.Info.Cadastrais.Application;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Tests.Fixtures;
using Gsl.Info.Cadastrais.Tests.Mocks;
using System;
using Xunit;
using System.Threading;

namespace Gsl.Info.Cadastrais.Tests.Controllers
{
    [Collection("Mapper")]
    public class UsuariosControllerTest
    {
        private readonly MapperFixture _mapperFixture;

        public UsuariosControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }

        #region Usuario

        /// <summary>
        /// Dado que:
        ///    seja solicitado o cadastro de um usuario
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se não existem no banco
        /// Para 
        ///    Armazenar as informações no banco
        /// </summary>
        [Fact]
        public async void CadastrarUsuario_Sucesso()
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController(true, true, true, false);

            var input = new UsuarioModel
            {
                Login = "123465",
                Senha = "123456"
            };

            //Act
            var result = await controller.CadastrarUsuario(input, ctx);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        /// <summary>
        /// Dado que:
        ///    seja solicitado o cadastro de um usuario
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se não existem no banco
        /// Para 
        ///    Informar o motivo do usuario não poder ser cadastrado 
        /// </summary>
        [Fact]
        public async void CadastrarUsuario_Erro()
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController(true, true, false);

            var input = new UsuarioModel
            {
                Login = "123465",
                Senha = "123456"
            };

            //Act
            var result = await controller.CadastrarUsuario(input, ctx);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        /// <summary>
        /// Dado que:
        ///    seja solicitado os dados de um usuario
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se existem no banco
        /// Para 
        ///    Retornar as informações do perfil identificado
        /// </summary>
        [Theory]
        [InlineData("123465")]
        [InlineData("90459735020")]
        public async void ObterUsuario_Sucesso(string login)
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController();

            //Act
            var result = await controller.ObterUsuario(login, "123456", ctx);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Dado que:
        ///    seja solicitado os dados de um usuario
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se existem no banco
        /// Para 
        ///    Retornar os motivos de não encontrar o usuario 
        /// </summary>
        [Theory]
        [InlineData("", false, false, "123456", true, true)]
        [InlineData("123465", false, false, "123456", true, true)]
        [InlineData("123465", true, false, "789456", true, true)]
        [InlineData("123465", true, false, "123456", true, false)]
        [InlineData("90459735020", true, false, "123456", false, true)]
        public async void ObterUsuario_Erro(string login, bool usuarioValido, bool usuarioExiste, string senha, bool clienteValido, bool operadorValido)
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController(clienteValido, operadorValido, usuarioValido, usuarioExiste);

            //Act
            var result = await controller.ObterUsuario(login, senha, ctx);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        #endregion

        #region Cliente

        /// <summary>
        /// Dado que:
        ///    seja solicitado o cadastro de um cliente
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se não existem no banco
        /// Para 
        ///    Armazenar as informações no banco
        /// </summary>
        [Fact]
        public async void CadastrarCliente_Sucesso()
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController();

            var endereco = new EnderecoFixture().CriarEndereco();

            var input = new ClienteModel
            {
                Aniversario = DateTime.Now.AddYears(-20),
                Cpf = "90459735020",
                Nome = "Teste",
                Endereco = new ClienteModel.DadosEndereco
                {
                    Cep = endereco.Cep,
                    Cidade = endereco.Cidade,
                    Complemento = endereco.Complemento,
                    Estado = endereco.Estado,
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero
                }
            };

            //Act
            var result = await controller.CadastrarCliente(input, ctx);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        /// <summary>
        /// Dado que:
        ///    seja solicitado o cadastro de um cliente
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se não existem no banco
        /// Para 
        ///    Informar o motivo do cliente não poder ser cadastrado 
        /// </summary>
        [Theory]
        [InlineData("12345678901", true)]
        [InlineData("90459735020", false)]
        public async void CadastrarCliente_Erro(string cpf, bool clienteValido)
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController(clienteValido);

            var endereco = new EnderecoFixture().CriarEndereco();

            var input = new ClienteModel
            {
                Aniversario = DateTime.Now.AddYears(-20),
                Cpf = cpf,
                Nome = "Teste",
                Endereco = new ClienteModel.DadosEndereco
                {
                    Cep = endereco.Cep,
                    Cidade = endereco.Cidade,
                    Complemento = endereco.Complemento,
                    Estado = endereco.Estado,
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero
                }
            };

            //Act
            var result = await controller.CadastrarCliente(input, ctx);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        #endregion

        #region Operador

        /// <summary>
        /// Dado que:
        ///    seja solicitado o cadastro de um operador
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se não existem no banco
        /// Para 
        ///    Armazenar as informações no banco
        /// </summary>
        [Fact]
        public async void CadastrarOperador_Sucesso()
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController();

            var input = new OperadorModel
            {
                Matricula = "123465",
                Nome = "Teste"
            };

            //Act
            var result = await controller.CadastrarOperador(input, ctx);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        /// <summary>
        /// Dado que:
        ///    seja solicitado o cadastro de um operador
        /// Eu preciso 
        ///    validar se os dados informados são válidos e se não existem no banco
        /// Para 
        ///    Informar o motivo do operador não poder ser cadastrado 
        /// </summary>
        [Fact]
        public async void CadastrarOperador_Erro()
        {
            //Prepare           
            var ctx = CancellationToken.None;
            var controller = CriarUsuarioController(true, false);

            var input = new OperadorModel
            {
                Matricula = "123465",
                Nome = "Teste"
            };

            //Act
            var result = await controller.CadastrarOperador(input, ctx);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        #endregion

        private UsuariosController CriarUsuarioController(bool clienteValido = true, bool operadorValido = true, bool usuarioValido = true, bool usuarioExiste = true)
        {
            var clienteRepository = new ClienteRepositoryMock().ObterPorCpf(clienteValido).Salvar().VerificarSeExiste(!clienteValido);
            var operadorRepository = new OperadorRepositoryMock().ObterPorMatricula(operadorValido).Salvar().VerificarSeExiste(!operadorValido);
            var usuarioRepository = new UsuarioRepositoryMock().ObterPorLogin(usuarioValido).Salvar().VerificarSeExiste(usuarioExiste);
            var usuarioApplication = new UsuarioApplication(_mapperFixture.Mapper, clienteRepository.Object, operadorRepository.Object, usuarioRepository.Object);

            return new UsuariosController(_mapperFixture.Mapper, usuarioApplication);
        }      
    }
}
