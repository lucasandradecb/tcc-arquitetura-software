using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Gsl.Info.Cadastrais.Api.Controllers.v1
{
    /// <summary>
    /// Controller de usuarios
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioApplication _usuarioApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="usuarioApplication"></param>
        public UsuariosController(IMapper mapper, IUsuarioApplication usuarioApplication)
        {
            _mapper = mapper;
            _usuarioApplication = usuarioApplication;
        }

        /// <summary>
        /// Realiza o cadastro de um usuario
        /// </summary>
        /// <param name="usuarioModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarUsuario(UsuarioModel usuarioModel, CancellationToken ctx)
        {
            var result = await _usuarioApplication.CadastrarUsuario(usuarioModel, ctx);

            if (result.Valid)
                return Ok();

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Obtem os dados do usuario
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UsuarioTipoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterUsuario([FromQuery, Required] string login, [FromQuery, Required] string senha, CancellationToken ctx)
        {
            var input = new UsuarioModel
            {
                Login = login,
                Senha = senha
            };

            var result = await _usuarioApplication.ObterUsuario(input, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost("clientes")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarCliente(ClienteModel clienteModel, CancellationToken ctx)
        {
            var result = await _usuarioApplication.CadastrarCliente(clienteModel, ctx);

            if (result.Valid)
                return Created("/usuarios/clientes", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de um operador
        /// </summary>
        /// <param name="operadorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost("operadores")]
        [ProducesResponseType(typeof(Operador), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarOperador(OperadorModel operadorModel, CancellationToken ctx)
        {
            var result = await _usuarioApplication.CadastrarOperador(operadorModel, ctx);

            if (result.Valid)
                return Created("/usuarios/operadores", result.Object);

            return UnprocessableEntity(result.Notifications);
        }
    }
}
