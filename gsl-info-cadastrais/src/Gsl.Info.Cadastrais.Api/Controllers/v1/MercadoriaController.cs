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
    /// Controller de mercadorias
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MercadoriaController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IMercadoriaApplication _mercadoriaApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="mercadoriaApplication"></param>
        public MercadoriaController(IMapper mapper, IMercadoriaApplication mercadoriaApplication)
        {
            _mapper = mapper;
            _mercadoriaApplication = mercadoriaApplication;
        }

        /// <summary>
        /// Retorna a lista de mercadorias cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<MercadoriaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodasMercadorias(CancellationToken ctx)
        {
            var result = await _mercadoriaApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }

        /// <summary>
        /// Obtem os dados da mercadoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(MercadoriaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterMercadoria([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _mercadoriaApplication.ObterMercadoria(codigo, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de uma mercadoria
        /// </summary>
        /// <param name="mercadoriaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Mercadoria), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarMercadoria(MercadoriaModel mercadoriaModel, CancellationToken ctx)
        {
            var result = await _mercadoriaApplication.CadastrarMercadoria(mercadoriaModel, ctx);

            if (result.Valid)
                return Created("/mercadorias", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a atualização de uma mercadoria
        /// </summary>
        /// <param name="mercadoriaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Mercadoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarMercadoria(MercadoriaModel mercadoriaModel, CancellationToken ctx)
        {
            var result = await _mercadoriaApplication.AtualizarMercadoria(mercadoriaModel, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Deleta uma mercadoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpDelete("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarMercadoria([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _mercadoriaApplication.DeletarMercadoria(codigo, ctx);

            if (result.Valid)
                return Ok();

            return UnprocessableEntity(result.Notifications);
        }
    }
}
