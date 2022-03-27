namespace Gsl.Gestao.Estrategica.Application.Models
{
    /// <summary>
    /// Modelo de dados de estoque
    /// </summary>
    public class EstoqueModel
    {
        /// Codigo do estoque
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Codigo do deposito
        /// </summary>
        public int DepositoCodigo { get; set; }
        /// <summary>
        /// Codigo da mercadoria
        /// </summary>
        public int MercadoriaCodigo { get; set; }
        /// <summary>
        /// Valor total do estoque
        /// </summary>
        public double ValorTotal { get; set; }
    }
}
