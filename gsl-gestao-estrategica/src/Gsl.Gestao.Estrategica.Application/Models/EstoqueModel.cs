using Gsl.Gestao.Estrategica.Infrastructure.Models;
using System.Collections.Generic;

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
        /// Lista de mercadorias do estoque
        /// </summary>
        public List<MercadoriaGatewayModel> ListaMercadorias { get; set; }
    }
}
