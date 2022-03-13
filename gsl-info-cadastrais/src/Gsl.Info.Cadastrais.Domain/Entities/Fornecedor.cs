using Flunt.Validations;
using Gsl.Info.Cadastrais.Domain.ValueObjects;
using System;
using Gsl.Info.Cadastrais.Domain.Entities.Core;

namespace Gsl.Info.Cadastrais.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de fornecedor
    /// </summary>
    public class Fornecedor : Entity
    {
        /// <summary>
        /// Construtor padrão de forncedor
        /// </summary>
        public Fornecedor() { }

        /// <summary>
        /// Construtor de fornecedor
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cnpj"></param>        
        /// <param name="endereco"></param>
        /// <param name="localizacao"></param>
        public Fornecedor(string nome, string cnpj, EnderecoCompleto endereco, LocalizacaoCompleta localizacao)
        {
            Nome = nome;
            Cnpj = cnpj;
            Endereco = endereco;
            Localizacao = localizacao;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Cnpj, nameof(Cnpj), "Cnpj não pode ser nulo")
                .IsNotNull(Endereco, nameof(Endereco), "Endereço não pode ser nulo")
                .IsNotNull(Localizacao, nameof(Localizacao), "Localização não pode ser nula"));
        }

        /// <summary>
        /// Nome do fornecedor
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cnpj do fornecedor
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Dados do endereço do fornecedor
        /// </summary>
        public EnderecoCompleto Endereco { get; set; }
        /// <summary>
        /// Dados de localização do fornecedor
        /// </summary>
        public LocalizacaoCompleta Localizacao { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
