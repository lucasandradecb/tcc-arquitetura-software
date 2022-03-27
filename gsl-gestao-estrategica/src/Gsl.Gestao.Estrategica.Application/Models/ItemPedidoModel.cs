namespace Gsl.Gestao.Estrategica.Application.Models
{
    /// <summary>
    /// Modelo de dados de item pedido
    /// </summary>
    public class ItemPedidoModel
    {
        /// <summary>
        /// Codigo do pedido
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Codigo da mercadoria
        /// </summary>
        public int MercadoriaCodigo { get; set; }
        /// <summary>
        /// Quantidade da mercadoria 
        /// </summary>
        public int MercadoriaQuantidade { get; set; }
        /// <summary>
        /// Valor total do item
        /// </summary>
        public double Valor { get; set; }
    }
}
