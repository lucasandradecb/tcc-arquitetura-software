using AutoMapper;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Domain.Resources;

namespace Gsl.Info.Cadastrais.Application
{
    /// <summary>
    /// Classe de application de veiculo
    /// </summary>
    public class VeiculoApplication : IVeiculoApplication
    {
        private readonly IMapper _mapper;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IMarcaVeiculoRepository _marcaVeiculoRepository;
        private readonly IModeloVeiculoRepository _modeloVeiculoRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="veiculoRepository"></param>
        /// <param name="marcaVeiculoRepository"></param>
        /// <param name="modeloVeiculoRepository"></param>
        /// <param name="agendamentoRepository"></param>
        public VeiculoApplication(IMapper mapper, 
                                  IVeiculoRepository veiculoRepository,
                                  IMarcaVeiculoRepository marcaVeiculoRepository,
                                  IModeloVeiculoRepository modeloVeiculoRepository,
                                  IAgendamentoRepository agendamentoRepository)
        {
            _mapper = mapper;
            _veiculoRepository = veiculoRepository;
            _marcaVeiculoRepository = marcaVeiculoRepository;
            _modeloVeiculoRepository = modeloVeiculoRepository;
            _agendamentoRepository = agendamentoRepository;
        }

        #region Veículo

        /// <summary>
        /// Realiza o cadastro de um veiculo
        /// </summary>
        /// <param name="veiculoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Veiculo>> CadastrarVeiculo(VeiculoModel veiculoModel, CancellationToken ctx)
        {
            var veiculo = _mapper.Map<VeiculoModel, Veiculo>(veiculoModel);  

            if (veiculo.Invalid)
                return Result<Veiculo>.Error(veiculo.Notifications);

            if (await _veiculoRepository.VerificarSeExiste(veiculo, ctx))
            {
                veiculo.AddNotification(nameof(Veiculo), MensagensInfo.Veiculo_PlacaExistente);
                return Result<Veiculo>.Error(veiculo.Notifications);
            }

            if (!await _marcaVeiculoRepository.VerificarSeExiste(new MarcaVeiculo(veiculo.CodigoMarca), ctx))
            {
                veiculo.AddNotification(nameof(Veiculo), MensagensInfo.Veiculo_MarcaNaoCadastrada);
                return Result<Veiculo>.Error(veiculo.Notifications);
            }

            if (!await _modeloVeiculoRepository.VerificarSeExiste(new ModeloVeiculo(veiculo.CodigoModelo), ctx))
            {
                veiculo.AddNotification(nameof(Veiculo), MensagensInfo.Veiculo_ModeloNaoCadastrado);
                return Result<Veiculo>.Error(veiculo.Notifications);
            }

            await _veiculoRepository.Salvar(veiculo, ctx);
            return Result<Veiculo>.Ok(veiculo);                 
        }

        /// <summary>
        /// Obtém a lista de veículos 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<VeiculoCompletoOutputModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaDadosCompletos = new List<VeiculoCompletoOutputModel>();
            var listaVeiculos = await _veiculoRepository.ListarTodos(ctx);

            foreach (var veiculo in listaVeiculos)
            {
                var dadosCompletos = _mapper.Map<VeiculoCompletoOutputModel>(veiculo);
                var marcaVeiculo = await _marcaVeiculoRepository.ObterPorCodigo(dadosCompletos.Marca, ctx);
                var modeloVeiculo = await _modeloVeiculoRepository.ObterPorCodigo(dadosCompletos.Modelo, ctx);

                dadosCompletos.DescricaoMarca = marcaVeiculo.Descricao;
                dadosCompletos.DescricaoModelo = modeloVeiculo.Descricao;
                listaDadosCompletos.Add(dadosCompletos);
            }

            return Result<List<VeiculoCompletoOutputModel>>.Ok(listaDadosCompletos);
        }

        #endregion

        #region Marca Veículo

        /// <summary>
        /// Realiza o cadastro de uma marca de veiculo
        /// </summary>
        /// <param name="marcaVeiculoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<MarcaVeiculo>> CadastrarMarcaVeiculo(MarcaVeiculoModel marcaVeiculoModel, CancellationToken ctx)
        {
            var marca = _mapper.Map<MarcaVeiculoModel, MarcaVeiculo>(marcaVeiculoModel);

            if (marca.Valid)
            {
                if (!await _marcaVeiculoRepository.VerificarSeExiste(marca, ctx))
                {
                    await _marcaVeiculoRepository.Salvar(marca, ctx);
                    return Result<MarcaVeiculo>.Ok(marca);
                }

                marca.AddNotification(nameof(MarcaVeiculo), MensagensInfo.MarcaVeiculo_CodigoExistente);
            }

            return Result<MarcaVeiculo>.Error(marca.Notifications);
        }

        #endregion

        #region Modelo Veículo

        /// <summary>
        /// Realiza o cadastro de um modelo de veiculo
        /// </summary>
        /// <param name="modeloVeiculoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<ModeloVeiculo>> CadastrarModeloVeiculo(ModeloVeiculoModel modeloVeiculoModel, CancellationToken ctx)
        {
            var modelo = _mapper.Map<ModeloVeiculoModel, ModeloVeiculo>(modeloVeiculoModel);

            if (modelo.Valid)
            {
                if (!await _modeloVeiculoRepository.VerificarSeExiste(modelo, ctx))
                {
                    await _modeloVeiculoRepository.Salvar(modelo, ctx);
                    return Result<ModeloVeiculo>.Ok(modelo);
                }

                modelo.AddNotification(nameof(ModeloVeiculo), MensagensInfo.ModeloVeiculo_CodigoExistente);
            }

            return Result<ModeloVeiculo>.Error(modelo.Notifications);
        }

        #endregion

        #region Simulação

        /// <summary>
        /// Realiza a simulação de um agendamento
        /// </summary>
        /// <param name="agendamentoInput"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<SimulacaoAgendamentoModel>> SimularAluguelVeiculo(AgendamentoInputModel agendamentoInput, CancellationToken ctx)
        {
            var listaInconsistencias = ValidarRegrasAgendamento(agendamentoInput.Placa, agendamentoInput.DataRetirada, agendamentoInput.DataDevolucao);

            if (listaInconsistencias.Any())
                return Result<SimulacaoAgendamentoModel>.Error(listaInconsistencias);

            var veiculo = await _veiculoRepository.ObterPorPlaca(agendamentoInput.Placa, ctx);
            if (veiculo == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Veiculo), MensagensInfo.Veiculo_PlacaNaoEncontrada) };
                return Result<SimulacaoAgendamentoModel>.Error(notification);
            }

            TimeSpan diferencaDatas = agendamentoInput.DataDevolucao.Subtract(agendamentoInput.DataRetirada);
            var totalHoras = diferencaDatas.TotalHours > 0 ? diferencaDatas.TotalHours : 1;

            var simulacao = new SimulacaoAgendamentoModel
            {
                Placa = veiculo.Placa,
                ValorHora = veiculo.ValorHora,
                ValorTotal = CalcularValorTotalAgendamento(totalHoras, veiculo.ValorHora),
                TotalHoras = totalHoras
            };

            return Result<SimulacaoAgendamentoModel>.Ok(simulacao);            
        }

        #endregion

        #region Agendamento

        /// <summary>
        /// Realiza o agendamento de um aluguel de veiculo
        /// </summary>
        /// <param name="agendamentoInput"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<AgendamentoOutputModel>> AgendarAluguelVeiculo(AgendamentoInputModel agendamentoInput, CancellationToken ctx)
        {
            var listaInconsistencias = ValidarRegrasAgendamento(agendamentoInput.Placa, agendamentoInput.DataRetirada, agendamentoInput.DataDevolucao);

            if (listaInconsistencias.Any())
                return Result<AgendamentoOutputModel>.Error(listaInconsistencias);

            var veiculo = await _veiculoRepository.ObterPorPlaca(agendamentoInput.Placa, ctx);
            if (veiculo == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Veiculo), MensagensInfo.Veiculo_PlacaNaoEncontrada) };
                return Result<AgendamentoOutputModel>.Error(notification);
            }

            TimeSpan diferencaDatas = agendamentoInput.DataDevolucao.Subtract(agendamentoInput.DataRetirada);
            var totalHoras = diferencaDatas.TotalHours > 0 ? diferencaDatas.TotalHours : 1;
            var valorTotal = CalcularValorTotalAgendamento(totalHoras, veiculo.ValorHora);

            var agendamento = new Agendamento(veiculo.Placa, veiculo.ValorHora, totalHoras, valorTotal, veiculo.Categoria, GerarCodigoReserva());

            await _agendamentoRepository.Salvar(agendamento, ctx);

            var output = _mapper.Map<Agendamento, AgendamentoOutputModel>(agendamento);

            return Result<AgendamentoOutputModel>.Ok(output);
        }

        #endregion

        #region Devolução

        /// <summary>
        /// Realiza a devolução do veiculo
        /// </summary>
        /// <param name="devolucaoInput"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<DevolucaoOutputModel>> DevolverVeiculo(DevolucaoInputModel devolucaoInput, CancellationToken ctx)
        {
            if (string.IsNullOrEmpty(devolucaoInput.CodigoReserva))
            {
                var notification = new List<Notification> { new Notification(nameof(devolucaoInput.CodigoReserva), MensagensInfo.Veiculo_ReservaInvalida) };
                return Result<DevolucaoOutputModel>.Error(notification);
            }

            if (string.IsNullOrEmpty(devolucaoInput.Placa))
            {
                var notification = new List<Notification> { new Notification(nameof(devolucaoInput.Placa), MensagensInfo.Veiculo_PlacaInvalida) };
                return Result<DevolucaoOutputModel>.Error(notification);
            }

            var agendamento = await _agendamentoRepository.ObterPorReserva(devolucaoInput.CodigoReserva, ctx);
            if (agendamento == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Agendamento), MensagensInfo.Veiculo_ReservaNaoEncontrada) };
                return Result<DevolucaoOutputModel>.Error(notification);
            }

            var valorTotal30PorCento = Math.Round((30.0 / 100.0) * agendamento.ValorTotalAluguel, 2);

            var valorAdicional = devolucaoInput.ItensVistoria.Amassados ? valorTotal30PorCento : 0;
            valorAdicional = devolucaoInput.ItensVistoria.Arranhoes ? valorAdicional + valorTotal30PorCento : valorAdicional;
            valorAdicional = devolucaoInput.ItensVistoria.CarroLimpo ? valorAdicional + valorTotal30PorCento : valorAdicional;
            valorAdicional = devolucaoInput.ItensVistoria.TanqueCheio ? valorAdicional + valorTotal30PorCento : valorAdicional;

            var outputModel = new DevolucaoOutputModel
            {
                Placa = agendamento.Placa,
                CodigoReserva = agendamento.CodigoReserva,
                ValorTotalReserva = agendamento.ValorTotalAluguel + valorAdicional,
                ItensReserva = new DevolucaoOutputModel.Itens
                {
                    TotalHoras = agendamento.TotalHoras,
                    ValorHora = agendamento.ValorHoraVeiculo,
                    ValorAdicionalVistoria = valorAdicional
                }
            };

            return Result<DevolucaoOutputModel>.Ok(outputModel);
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Valida regras de agendamento para verificar se os dados necessários para o cálculo foram informados corretamente
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="dataRetirada"></param>
        /// <param name="dataDevolucao"></param>
        /// <returns></returns>
        private List<Notification> ValidarRegrasAgendamento(string placa, DateTime dataRetirada, DateTime dataDevolucao)
        {
            var notificacoes = new List<Notification>();

            if (string.IsNullOrEmpty(placa))
                notificacoes.Add(new Notification(nameof(placa), MensagensInfo.Veiculo_PlacaInvalida));

            if (dataRetirada == DateTime.MinValue || dataDevolucao == DateTime.MinValue)
                notificacoes.Add(new Notification("Datas", MensagensInfo.Agendamento_DatasInvalidas));

            if (dataRetirada > dataDevolucao)
                notificacoes.Add(new Notification("Datas", MensagensInfo.Agendamento_DataDevMenorRet));

            return notificacoes;
        }

        /// <summary>
        /// Gera um valor aleatório de código de reserva
        /// </summary>
        /// <returns></returns>
        private string GerarCodigoReserva()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 10)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        /// <summary>
        /// Realiza o cálculo do valor total do agendamento considerando 
        /// o total de hors de aluguel e o valor da hora do veículo
        /// </summary>
        /// <param name="totalHoras"></param>
        /// <param name="valorHora"></param>
        /// <returns></returns>
        private double CalcularValorTotalAgendamento(double totalHoras, double valorHora)
        {            
            return Math.Round(valorHora * totalHoras, 2);
        }

        #endregion
    }
}
