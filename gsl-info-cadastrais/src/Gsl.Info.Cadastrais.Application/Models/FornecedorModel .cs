using System;
using Gsl.Info.Cadastrais.Domain.ValueObjects;

namespace Gsl.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados de fornecedor
    /// </summary>
    public class FornecedorModel
    {
        /// <summary>
        /// Nome do fornecedor
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cnpj do forncedor
        /// </summary>
        public string Cnpj { get; set; }        
        /// <summary>
        /// Dados do endereço do fornecedor
        /// </summary>
        public DadosEnderecoFornecedor Endereco { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Dados de endereco
        /// </summary>
        public class DadosEnderecoFornecedor
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
