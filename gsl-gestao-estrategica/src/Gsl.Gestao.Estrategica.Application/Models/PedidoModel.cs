using System;
using System.Collections.Generic;

namespace Gsl.Gestao.Estrategica.Application.Models
{
    /// <summary>
    /// Modelo de dados de pedido
    /// </summary>
    public class PedidoModel
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public PedidoModel()
        {
            ItensPedido = new List<ItemPedidoModel>();
        }

        /// <summary>
        /// Id do pedido
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Codigo do pedido
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Cof do cliente
        /// </summary>
        public string ClienteCpf { get; set; }
        /// <summary>
        /// Lista de Itens de um pedido
        /// </summary>
        public List<ItemPedidoModel> ItensPedido { get; set; }
        /// <summary>
        /// Valor total do pedido
        /// </summary>
        public double ValorTotal { get; set; }
    }
}
