using Flunt.Validations;
using System;
using Gsl.Info.Cadastrais.Domain.Entities.Core;
using Gsl.Info.Cadastrais.Domain.Enums;

namespace Gsl.Info.Cadastrais.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de Agendamento
    /// </summary>
    public class Agendamento : Entity
    {
        /// <summary>
        /// Construtor padrão de operador
        /// </summary>
        public Agendamento() { }

        /// <summary>
        /// Construtor de agendamento
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="valorHora"></param>
        /// <param name="totalHoras"></param>
        /// <param name="valorTotal"></param>
        /// <param name="categoria"></param>
        /// <param name="codigoReserva"></param>
        public Agendamento(string placa, double valorHora, double totalHoras, double valorTotal, ECategoriaVeiculo categoria, string codigoReserva)
        {
            Placa = placa;
            ValorHoraVeiculo = valorHora;
            TotalHoras = totalHoras;
            ValorTotalAluguel = valorTotal;
            CategoriaVeiculo = categoria;
            CodigoReserva = codigoReserva;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(placa, nameof(placa), "Placa não pode ser nula ou vazia"));
        }

        /// <summary>
        /// Placa do veiculo
        /// </summary>
        public string Placa { get; set; }
        /// <summary>
        /// Valor da hora de aluguel do veiculo
        /// </summary>
        public double ValorHoraVeiculo { get; set; }
        /// <summary>
        /// Total de horas de aluguel do veiculo
        /// </summary>
        public double TotalHoras { get; set; }
        /// <summary>
        /// Valor da hora de aluguel do veiculo
        /// </summary>
        public double ValorTotalAluguel { get; set; }
        /// <summary>
        /// Categoria do veiculo
        /// </summary>
        public ECategoriaVeiculo CategoriaVeiculo { get; set; }
        /// <summary>
        /// Codigo da reserva
        /// </summary>
        public string CodigoReserva { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
