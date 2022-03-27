using Flunt.Notifications;
using System;

namespace Gsl.Gestao.Estrategica.Infrastructure.Models
{
    /// <summary>
    /// Modelo de dados da mercadoria
    /// </summary>
    public class MercadoriaGatewayModel : Notifiable
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
        /// Identificador do fornecedor
        /// </summary>
        public Guid FornecedorId { get; set; }
    }
}
