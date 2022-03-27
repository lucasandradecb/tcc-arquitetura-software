using System;
using System.Collections.Generic;
using Gsl.Gestao.Estrategica.Domain.Entities.Core;
using Gsl.Gestao.Estrategica.Domain.Enums;

namespace Gsl.Gestao.Estrategica.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de Entrega
    /// </summary>
    public class Entrega : Entity
    {
        /// <summary>
        /// Construtor padrão de Entrega
        /// </summary>
        public Entrega() { }

        /// <summary>
        /// Construtor de entrega
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="pedidoId"></param>
        /// <param name="latitudeEntrega"></param>
        /// <param name="longitudeEntrega"></param>
        /// <param name="statusEntrega"></param>
        public Entrega(string codigo, Guid pedidoId, double latitudeEntrega, double longitudeEntrega, EStatusEntrega statusEntrega)
        {
            Codigo = codigo;
            PedidoId = pedidoId;
            LatitudeEntrega = latitudeEntrega;
            LongitudeEntrega = longitudeEntrega;
            StatusEntrega = statusEntrega;
            DataCriacao = DateTime.UtcNow;      
        }

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
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
