using Flunt.Validations;
using System;
using Gsl.Info.Cadastrais.Domain.Entities.Core;

namespace Gsl.Info.Cadastrais.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de operador
    /// </summary>
    public class Operador : Entity
    {
        /// <summary>
        /// Construtor padrão de operador
        /// </summary>
        public Operador() { }

        /// <summary>
        /// Construtor de operador
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="matricula"></param>
        public Operador(string nome, string matricula)
        {
            Nome = nome;
            Matricula = matricula;
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Matricula, nameof(Matricula), "Matricula não pode ser nula"));
        }

        /// <summary>
        /// Nome do operador
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Matricula do operador
        /// </summary>
        public string Matricula { get; set; }        
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }        
    }
}
