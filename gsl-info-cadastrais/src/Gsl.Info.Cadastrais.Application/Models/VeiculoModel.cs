using System.Diagnostics.CodeAnalysis;
using Gsl.Info.Cadastrais.Domain.Enums;

namespace Gsl.Info.Cadastrais.Application.Models
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Modelo de dados de um veículo
    /// </summary>
    public class VeiculoModel
    {
        /// <summary>
        /// Placa do veiculo
        /// </summary>
        public string Placa { get; set; }
        /// <summary>
        /// Marca do veiculo
        /// </summary>
        public string Marca { get; set; }
        /// <summary>
        /// MOdelo do veiculo
        /// </summary>
        public string Modelo { get; set; }
        /// <summary>
        /// Ano do veiculo
        /// </summary>
        public int Ano { get; set; }
        /// <summary>
        /// Valor da hora de aluguel do veiculo
        /// </summary>
        public double ValorHora { get; set; }
        /// <summary>
        /// Tipo de combustivel do veiculo
        /// </summary>
        public ETipoCombustivel Combustivel { get; set; }
        /// <summary>
        /// Limite do porta malas do veiculo
        /// </summary>
        public double LimitePortaMalas { get; set; }
        /// <summary>
        /// Categoria do veiculo
        /// </summary>
        public ECategoriaVeiculo Categoria { get; set; }
    }
}
