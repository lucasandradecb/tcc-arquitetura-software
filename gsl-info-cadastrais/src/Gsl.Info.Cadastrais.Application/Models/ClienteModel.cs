using System;
using Gsl.Info.Cadastrais.Domain.ValueObjects;

namespace Gsl.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados de cliente
    /// </summary>
    public class ClienteModel
    {
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cpf do cliente
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// Data de aniversário do cliente
        /// </summary>
        public DateTime Aniversario { get; set; }
        /// <summary>
        /// Dados do endereço do cliente
        /// </summary>
        public DadosEndereco Endereco { get; set; }

        /// <summary>
        /// Dados de endereco
        /// </summary>
        public class DadosEndereco
        {
             /// <summary>
             /// CEP do endereço
             /// </summary>
            public string Cep { get; set; }
            /// <summary>
            /// Descrição do logradouro (Rua, AV...)
            /// </summary>
            public string Logradouro { get; set; }
            /// <summary>
            /// Número do logradouro
            /// </summary>
            public string Numero { get; set; }
            /// <summary>
            /// Complemento do logradouro
            /// </summary>
            public string Complemento { get; set; }
            /// <summary>
            /// Nome da Cidade
            /// </summary>
            public string Cidade { get; set; }
            /// <summary>
            /// Nome do Estado
            /// </summary>
            public string Estado { get; set; }        
        }
    }
}
