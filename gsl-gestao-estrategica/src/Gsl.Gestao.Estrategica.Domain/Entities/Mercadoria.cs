﻿using Gsl.Gestao.Estrategica.Domain.Entities.Core;
using System;

namespace Gsl.Gestao.Estrategica.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de mercadoria
    /// </summary>
    public class Mercadoria : Entity
    {
        /// <summary>
        /// Construtor padrão de Mercadoria
        /// </summary>
        public Mercadoria() { }

        /// <summary>
        /// Construtor de mercadoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nome"></param>
        /// <param name="valor"></param>     
        /// <param name="quantidade"></param>
        /// <param name="fornecedorId"></param>
        public Mercadoria(int codigo, string nome, double valor, int quantidade, Guid fornecedorId)
        {
            Codigo = codigo;
            Nome = nome;
            Valor = valor;
            Quantidade = quantidade;
            FornecedorId = fornecedorId;    
        }

        /// <summary>
        /// Codigo de identificação da mercadoria
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Nome da mercadoria
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Valor da mercadoria
        /// </summary>
        public double Valor { get; set; }
        /// <summary>
        /// quantidade de mercadoria
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// Id do fornecedor
        /// </summary>
        public Guid FornecedorId { get; set; }
    }
}
