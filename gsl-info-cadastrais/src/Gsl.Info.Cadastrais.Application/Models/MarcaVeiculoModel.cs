using System.Diagnostics.CodeAnalysis;

namespace Gsl.Info.Cadastrais.Application.Models
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Modelo de dados da marca de um veículo
    /// </summary>
    public class MarcaVeiculoModel
    {
        /// <summary>
        /// Codigo da marca
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descrição da marca
        /// </summary>
        public string Descricao { get; set; }       
    }
}
