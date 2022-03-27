using System;
using System.Collections.Generic;
using Gsl.Gestao.Estrategica.Domain.Entities.Core;

namespace Gsl.Gestao.Estrategica.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de Pedido
    /// </summary>
    public class Pedido : Entity
    {
        /// <summary>
        /// Construtor padrão de Pedido
        /// </summary>
        public Pedido() { }

        /// <summary>
        /// Construtor de pedido
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="clienteCpf"></param>
        /// <param name="valorTotal"></param>
        public Pedido(int codigo, string clienteCpf, double valorTotal)
        {
            Codigo = codigo;
            ClienteCpf = clienteCpf;
            ValorTotal = valorTotal;
            DataCriacao = DateTime.UtcNow;      
        }

        /// <summary>
        /// Codigo do pedido
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Cof do cliente
        /// </summary>
        public string ClienteCpf { get; set; }
        /// <summary>
        /// Item de um pedido
        /// </summary>
        public IEnumerable<ItemPedido> ItensPedido { get; set; }        
        /// <summary>
        /// Valor total do pedido
        /// </summary>
        public double ValorTotal { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
