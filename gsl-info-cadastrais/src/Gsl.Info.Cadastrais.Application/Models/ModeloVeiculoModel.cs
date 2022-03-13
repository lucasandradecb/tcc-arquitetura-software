using System.Diagnostics.CodeAnalysis;

namespace Gsl.Info.Cadastrais.Application.Models
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Modelo de dados do modelo de um veículo
    /// </summary>
    public class ModeloVeiculoModel
    {
        /// <summary>
        /// Codigo do modelo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descrição do modelo
        /// </summary>
        public string Descricao { get; set; }       
    }
}
