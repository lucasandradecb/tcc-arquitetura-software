using Flunt.Notifications;
using System;

namespace Gsl.Gestao.Estrategica.Infrastructure.Models
{
    /// <summary>
    /// Modelo de dados de cliente
    /// </summary>
    public class ClienteGatewayModel : Notifiable
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
        public DadosEnderecoModel Endereco { get; set; }        
    }
}
