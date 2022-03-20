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
        /// Construtor padrão de fornecedor
        /// </summary>
        public Fornecedor() { }

        /// <summary>
        /// Construtor de fornecedor
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cnpj"></param>        
        /// <param name="endereco"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public Fornecedor(string nome, string cnpj, EnderecoCompleto endereco, double latitude, double longitude)
        {
            Nome = nome;
            Cnpj = cnpj;
            Endereco = endereco;
            Latitude = latitude;
            Longitude = longitude;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Cnpj, nameof(Cnpj), "Cnpj não pode ser nulo")
                .IsNotNull(Latitude, nameof(Latitude), "Latitude não pode ser nula")
                .IsNotNull(Longitude, nameof(Longitude), "Longitude não pode ser nula"));
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
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
