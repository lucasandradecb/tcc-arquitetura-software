using System;
using Gsl.Gestao.Estrategica.Domain.Entities.Core;

namespace Gsl.Gestao.Estrategica.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de Estoque
    /// </summary>
    public class Estoque : Entity
    {
        /// <summary>
        /// Construtor padrão de Estoque
        /// </summary>
        public Estoque() { }

        /// <summary>
        /// Construtor de Estoque
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="depositoCodigo"></param>
        /// <param name="mercadoriaCodigo"></param>
        /// <param name="valorTotal"></param>
        public Estoque(int codigo, int depositoCodigo, int mercadoriaCodigo, double valorTotal)
        {
            Codigo = codigo;
            DepositoCodigo = depositoCodigo;
            MercadoriaCodigo = mercadoriaCodigo;
            ValorTotal = valorTotal;
            DataCriacao = DateTime.UtcNow;      
        }

        /// <summary>
        /// Codigo do estoque
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Codigo do deposito
        /// </summary>
        public int DepositoCodigo { get; set; }
        /// <summary>
        /// Codigo da mercadoria
        /// </summary>
        public int MercadoriaCodigo { get; set; }
        /// <summary>
        /// Valor total do estoque
        /// </summary>
        public double ValorTotal { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
