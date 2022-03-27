using System;
using System.Collections.Generic;
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
        /// <param name="listaMercadorias"></param>
        public Estoque(int codigo, int depositoCodigo, List<Mercadoria> listaMercadorias)
        {
            Codigo = codigo;
            DepositoCodigo = depositoCodigo;
            ListaMercadorias = listaMercadorias;
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
        public List<Mercadoria> ListaMercadorias { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
