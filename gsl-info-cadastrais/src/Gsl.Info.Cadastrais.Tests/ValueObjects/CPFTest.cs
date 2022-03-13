using Gsl.Info.Cadastrais.Domain.ValueObjects;
using Xunit;

namespace Gsl.Info.Cadastrais.Tests.ValueObjects
{
    /// <summary>
    /// Testes unitários do value object de CPF
    /// </summary>
    public class CPFTest
    {
        /// <summary>
        /// Dado que:
        ///    Um número de cpf tenha sido informado
        /// Eu preciso 
        ///    Validar o cpf
        /// Para 
        ///    Informar o motivo do cpf não ser válido 
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("123456789123")]
        [InlineData("123456789101231")]
        [InlineData("asdfghjkloi")]
        [InlineData("1234567890a")]
        public void CriarCpf_CpfInvalido_Test(string numero)
        {
            var cpf = new CPF(numero);

            Assert.True(cpf.Invalid);
        }

        /// <summary>
        /// Dado que:
        ///    Um número de cpf tenha sido informado
        /// Eu preciso 
        ///    Validar o cpf
        /// Para 
        ///    Informar que o número é um cpf válido 
        /// </summary>
        [Theory]
        [InlineData("01921216000")]
        [InlineData("90459735020")]
        public void CriarCpf_CpfValido_Test(string numero)
        {
            var cpf = new CPF(numero);

            Assert.True(cpf.Valid);
        }        
    }
}