using AutoMapper;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Info.Cadastrais.Api.Controllers.v1
{
    /// <summary>
    /// Controller de fornecedores
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FornecedoresController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorApplication _fornecedorApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="fornecedorApplication"></param>
        public FornecedoresController(IMapper mapper, IFornecedorApplication fornecedorApplication)
        {
            _mapper = mapper;
            _fornecedorApplication = fornecedorApplication;
        }

        /// <summary>
        /// Retorna a lista de clientes cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<FornecedorModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodosFornecedores(CancellationToken ctx)
        {
            var result = await _fornecedorApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }

        /// <summary>
        /// Obtem os dados do fornecedor
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{cnpj}")]
        [ProducesResponseType(typeof(FornecedorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterFornecedor([FromRoute, Required] string cnpj, CancellationToken ctx)
        {
            var result = await _fornecedorApplication.ObterFornecedor(cnpj, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de um fornecedor
        /// </summary>
        /// <param name="fornecedorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Fornecedor), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarFornecedor(FornecedorModel fornecedorModel, CancellationToken ctx)
        {
            var result = await _fornecedorApplication.CadastrarFornecedor(fornecedorModel, ctx);

            if (result.Valid)
                return Created("/fornecedores", result.Object);

            return UnprocessableEntity(result.Notifications);
        }
    }
}
