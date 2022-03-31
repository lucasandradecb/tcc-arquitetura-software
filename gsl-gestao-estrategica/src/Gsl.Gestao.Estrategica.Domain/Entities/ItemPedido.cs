using System;
using System.Collections.Generic;
using Gsl.Gestao.Estrategica.Domain.Entities.Core;

namespace Gsl.Gestao.Estrategica.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de ItemPedido
    /// </summary>
    public class ItemPedido : Entity
    {
        /// <summary>
        /// Construtor padrão de ItemPedido
        /// </summary>
        public ItemPedido() { }

        /// <summary>
        /// Construtor de ItemPedido
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="mercadoriaCodigo"></param>
        /// <param name="mercadoriaQuantidade"></param>
        /// <param name="valor"></param>
        public ItemPedido(int codigo, int mercadoriaCodigo, int mercadoriaQuantidade, double valor)
        {
            Codigo = codigo;
            MercadoriaCodigo = mercadoriaCodigo;
            MercadoriaQuantidade = mercadoriaQuantidade;
            Valor = valor;
            DataCriacao = DateTime.UtcNow;      
        }

        /// <summary>
        /// Codigo do item pedido
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Codigo da mercadoria
        /// </summary>
        public int MercadoriaCodigo { get; set; }
        /// <summary>
        /// Quantidade da mercadoria 
        /// </summary>
        public int MercadoriaQuantidade { get; set; }
        /// <summary>
        /// Valor total do item
        /// </summary>
        public double Valor { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Id do pedido
        /// </summary>
        public Guid PedidoId { get; set; }
    }
}
