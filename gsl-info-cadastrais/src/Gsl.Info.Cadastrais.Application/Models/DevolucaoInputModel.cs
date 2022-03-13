namespace Gsl.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados entrada do checklist de devolução
    /// </summary>
    public class DevolucaoInputModel
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
        /// Itens de vistoria para indenização
        /// </summary>
        public VistoriaIndenizacao ItensVistoria { get; set; }

        /// <summary>
        /// Indenização de devolução
        /// </summary>
        public class VistoriaIndenizacao
        {
            /// <summary>
            /// Verifica se o carro esta limpo
            /// </summary>
            public bool CarroLimpo { get; set; }
            /// <summary>
            /// Verifica se o tanque esta cheio
            /// </summary>
            public bool TanqueCheio { get; set; }
            /// <summary>
            /// Verifica se o veiculo possui amassados
            /// </summary>
            public bool Amassados { get; set; }
            /// <summary>
            /// Verifica se o veiculo possui arranhoes
            /// </summary>
            public bool Arranhoes { get; set; }
        }
    }
}
