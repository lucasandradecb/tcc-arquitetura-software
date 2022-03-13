using Gsl.Info.Cadastrais.Domain.ValueObjects;
using Xunit;

namespace Gsl.Info.Cadastrais.Tests.ValueObjects
{
    /// <summary>
    /// Testes unitários do value object de endereço
    /// </summary>
    public class EnderecoCompletoTest
    {
        /// <summary>
        /// Dado que:
        ///    Informações de endereço tenham sido enviadas
        /// Eu preciso 
        ///    Validar os dados recebidos
        /// Para 
        ///    Informar o motivo do endereço passado não ser válido
        /// </summary>
        [Theory]
        [InlineData(null, "Rua Teste", "7", "Casa", "BH", "MG")]
        [InlineData("33900788", null, "7", "Casa", "BH", "MG")]
        [InlineData("33900788", "Rua Teste", null, "Casa", "BH", "MG")]
        [InlineData("33900788", "Rua Teste", "7", "Casa", null, "MG")]
        [InlineData("33900788", "Rua Teste", "7", "Casa", "BH", null)]
        public void CriarEndereco_Invalido_Test(string cep, string logradouro, string numero, string complemento, string cidade, string estado)
        {
            var endereco = new EnderecoCompleto(cep, logradouro, numero, complemento, cidade, estado);

            Assert.True(endereco.Invalid);
        }

        /// <summary>
        /// Dado que:
        ///    Informações de endereço tenham sido enviadas
        /// Eu preciso 
        ///    Validar os dados recebidos
        /// Para 
        ///    Informar que o endereço passado é válido
        /// </summary>
        [Theory]
        [InlineData("33900788", "Rua Teste", "7", "Casa", "BH", "MG")]
        [InlineData("33900788", "Rua Teste", "7", null, "BH", "MG")]
        public void CriarEndereco_Valido_Test(string cep, string logradouro, string numero, string complemento, string cidade, string estado)
        {
            var endereco = new EnderecoCompleto(cep, logradouro, numero, complemento, cidade, estado);

            Assert.True(endereco.Valid);
        }
    }
}