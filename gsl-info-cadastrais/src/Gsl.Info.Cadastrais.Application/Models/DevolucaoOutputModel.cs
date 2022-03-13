using System.Diagnostics.CodeAnalysis;

namespace Gsl.Info.Cadastrais.Application.Models
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Modelo de dados saida do checklist de devolução
    /// </summary>
    public class DevolucaoOutputModel
    {
        /// <summary>
        /// Codigo de reserva do agendamento
        /// </summary>
        public string CodigoReserva { get; set; }
        /// <summary>
        /// Placa do veiculo
        /// </summary>
        public string Placa { get; set; } 
        /// <summary>
        /// Valor da hora de aluguel do veiculo
        /// </summary>
        public double ValorTotalReserva { get; set; }
        /// <summary>
        /// Valor da hora de aluguel do veiculo
        /// </summary>
        public Itens ItensReserva { get; set; }

        /// <summary>
        /// Itens da reserva
        /// </summary>
        public class Itens
        {
            /// <summary>
            /// Valor da hora de aluguel do veiculo
            /// </summary>
            public double ValorHora { get; set; }
            /// <summary>
            /// Total de horas de aluguel do veiculo
            /// </summary>
            public double TotalHoras { get; set; }
            /// <summary>
            /// Valor adicionado ao aluguel devido a vistoria na devolução
            /// </summary>
            public double ValorAdicionalVistoria { get; set; }
        }
    }
}
