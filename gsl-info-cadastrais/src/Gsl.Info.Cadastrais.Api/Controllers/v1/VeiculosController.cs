using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Gsl.Info.Cadastrais.Api.Controllers.v1
{
    /// <summary>
    /// Controller de veiculos
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VeiculosController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IVeiculoApplication _veiculoApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="veiculoApplication"></param>
        public VeiculosController(IMapper mapper, IVeiculoApplication veiculoApplication)
        {
            _mapper = mapper;
            _veiculoApplication = veiculoApplication;
        }

        /// <summary>
        /// Realiza o cadastro de um veículo
        /// </summary>
        /// <param name="veiculoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Veiculo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarVeiculo(VeiculoModel veiculoModel, CancellationToken ctx)
        {
            var result = await _veiculoApplication.CadastrarVeiculo(veiculoModel, ctx);

            if (result.Valid)
                return Created("/veiculos", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de uma marca de veículo
        /// </summary>
        /// <param name="marcaVeiculoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost("marcas")]
        [ProducesResponseType(typeof(MarcaVeiculo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarMarcaVeiculo(MarcaVeiculoModel marcaVeiculoModel, CancellationToken ctx)
        {
            var result = await _veiculoApplication.CadastrarMarcaVeiculo(marcaVeiculoModel, ctx);

            if (result.Valid)
                return Created("/veiculos/marcas", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de um modelo de veículo
        /// </summary>
        /// <param name="modeloVeiculoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost("modelos")]
        [ProducesResponseType(typeof(ModeloVeiculo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarModeloVeiculo(ModeloVeiculoModel modeloVeiculoModel, CancellationToken ctx)
        {
            var result = await _veiculoApplication.CadastrarModeloVeiculo(modeloVeiculoModel, ctx);

            if (result.Valid)
                return Created("/veiculos/modelos", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o agendamento de um veiculo
        /// </summary>
        /// <param name="agendamentoInput"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost("agendamentos")]
        [ProducesResponseType(typeof(AgendamentoOutputModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgendarVeiculo(AgendamentoInputModel agendamentoInput, CancellationToken ctx)
        {
            var result = await _veiculoApplication.AgendarAluguelVeiculo(agendamentoInput, ctx);

            if (result.Valid)
                return Created($"/veiculos/{agendamentoInput.Placa}/agendamentos", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a simulação do valor de locação do veículo
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="dataRetirada"></param>
        /// <param name="dataDevolucao"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{placa}/simulacoes-locacao")]
        [ProducesResponseType(typeof(SimulacaoAgendamentoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SimularAgendamentoVeiculo([FromRoute] string placa, [FromQuery] DateTime dataRetirada, [FromQuery] DateTime dataDevolucao, CancellationToken ctx)
        {
            var input = new AgendamentoInputModel
            {
                DataDevolucao = dataDevolucao,
                DataRetirada = dataRetirada,
                Placa = placa
            };

            var result = await _veiculoApplication.SimularAluguelVeiculo(input, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a devolução de um veiculo
        /// </summary>
        /// <param name="devolucaoInput"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost("devolucoes")]
        [ProducesResponseType(typeof(DevolucaoOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DevolverVeiculo(DevolucaoInputModel devolucaoInput, CancellationToken ctx)
        {
            var result = await _veiculoApplication.DevolverVeiculo(devolucaoInput, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Retorna a lista de veiculos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<VeiculoCompletoOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodosVeiculos(CancellationToken ctx)
        {
            var result = await _veiculoApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }
    }
}
