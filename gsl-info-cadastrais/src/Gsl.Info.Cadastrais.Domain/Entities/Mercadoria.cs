using Flunt.Validations;
using System;
using Gsl.Info.Cadastrais.Domain.Entities.Core;

namespace Gsl.Info.Cadastrais.Domain.Entities
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
        public Mercadoria(int codigo, string nome, double valor, int quantidade)
        {
            Codigo = codigo;
            Nome = nome;
            Valor = valor;
            Quantidade = quantidade;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo"));
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
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
