namespace Gsl.Info.Cadastrais.Application.Models
{
    /// <summary>
    /// Modelo de dados de deposito
    /// </summary>
    public class DepositoModel
    {
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
        public DadosEnderecoModel Endereco { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public string Longitude { get; set; }       
    }
}
