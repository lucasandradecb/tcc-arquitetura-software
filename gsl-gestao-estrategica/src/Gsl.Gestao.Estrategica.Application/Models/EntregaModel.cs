using Gsl.Gestao.Estrategica.Domain.Enums;
using System;

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
        /// Id do pedido
        /// </summary>
        public Guid PedidoId { get; set; }
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
