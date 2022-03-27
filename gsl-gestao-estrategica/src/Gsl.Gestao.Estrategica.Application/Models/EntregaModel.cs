using Gsl.Gestao.Estrategica.Domain.Enums;

namespace Gsl.Gestao.Estrategica.Application.Models
{
    /// <summary>
    /// Modelo de dados de entraga
    /// </summary>
    public class EntregaModel
    {
        /// <summary>
        /// Codigo da entrega
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Codigo do pedido
        /// </summary>
        public int PedidoCodigo { get; set; }
        /// <summary>
        /// Latitude de localização da entrega
        /// </summary>
        public double LatitudeEntrega { get; set; }
        /// <summary>
        /// Longitude de localização da entrega
        /// </summary>
        public double LongitudeEntrega { get; set; }
        /// <summary>
        /// Status da entrega
        /// </summary>
        public EStatusEntrega StatusEntrega { get; set; }
    }
}
