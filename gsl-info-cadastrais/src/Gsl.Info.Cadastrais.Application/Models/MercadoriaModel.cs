using System;

namespace Gsl.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados da mercadoria
    /// </summary>
    public class MercadoriaModel
    {
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
        /// Quantidade da mercadoria
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// Id do Fornecedor
        /// </summary>
        public Guid FornecedorId { get; set; }
    }
}
