using System;

namespace Gsl.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados entrada de agendamento
    /// </summary>
    public class AgendamentoInputModel
    {        
        /// <summary>
        /// Placa do veiculo
        /// </summary>
        public string Placa { get; set; }
        /// <summary>
        /// Data de retirada do veículo
        /// </summary>
        public DateTime DataRetirada { get; set; }
        /// <summary>
        /// Data de devolução do veículo
        /// </summary>
        public DateTime DataDevolucao { get; set; }
    }
}
