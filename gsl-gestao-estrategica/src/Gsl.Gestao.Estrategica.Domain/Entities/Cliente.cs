using Flunt.Validations;
using Gsl.Gestao.Estrategica.Domain.ValueObjects;
using System;
using Gsl.Gestao.Estrategica.Domain.Entities.Core;

namespace Gsl.Gestao.Estrategica.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de cliente
    /// </summary>
    public class Cliente : Entity
    {
        /// <summary>
        /// Construtor padrão de cliente
        /// </summary>
        public Cliente() { }

        /// <summary>
        /// Construtor de cliente
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="aniversario"></param>
        /// <param name="endereco"></param>
        public Cliente(string nome, CPF cpf, DateTime aniversario, EnderecoCompleto endereco)
        {
            Nome = nome;
            Cpf = cpf;
            Aniversario = aniversario;
            Endereco = endereco;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Cpf, nameof(Cpf), "Cpf não pode ser nulo")
                .IsNotNull(Aniversario, nameof(Aniversario), "Data de aniversário não pode ser nula")
                .IsNotNull(Endereco, nameof(Endereco), "Endereço não pode ser nulo"));
        }

        /// <summary>
        /// Nome do Cliente
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cpf do cliente
        /// </summary>
        public CPF Cpf { get; set; }
        /// <summary>
        /// Data de aniversário do cliente
        /// </summary>
        public DateTime Aniversario { get; set; }
        /// <summary>
        /// Dados do endereço do cliente
        /// </summary>
        public EnderecoCompleto Endereco { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
