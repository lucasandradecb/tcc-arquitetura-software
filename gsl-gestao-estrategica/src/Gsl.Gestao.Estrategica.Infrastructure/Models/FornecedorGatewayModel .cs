using Flunt.Notifications;

namespace Gsl.Gestao.Estrategica.Infrastructure.Models
{
    /// <summary>
    /// Modelo de dados de fornecedor
    /// </summary>
    public class FornecedorGatewayModel : Notifiable
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
        public DadosEnderecoModel Endereco { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }       
    }
}
