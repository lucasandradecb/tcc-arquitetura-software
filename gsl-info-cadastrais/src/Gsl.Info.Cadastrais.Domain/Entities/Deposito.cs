using Flunt.Validations;
using Gsl.Info.Cadastrais.Domain.ValueObjects;
using System;
using Gsl.Info.Cadastrais.Domain.Entities.Core;

namespace Gsl.Info.Cadastrais.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de Deposito
    /// </summary>
    public class Deposito : Entity
    {
        /// <summary>
        /// Construtor padrão de Deposito
        /// </summary>
        public Deposito() { }

        /// <summary>
        /// Construtor de Deposito
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="codigo"></param>
        /// <param name="endereco"></param> 
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public Deposito(string nome, int codigo, EnderecoCompleto endereco, double latitude, double longitude)
        {
            Nome = nome;
            Codigo = codigo;
            Endereco = endereco;
            Latitude = latitude;
            Longitude = longitude;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Latitude, nameof(Latitude), "Latitude não pode ser nula")
                .IsNotNull(Longitude, nameof(Longitude), "Longitude não pode ser nula"));
        }

        /// <summary>
        /// Codigo de identificação do deposito
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Nome do deposito
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Dados do endereço do deposito
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
