using Gsl.Info.Cadastrais.Domain.ValueObjects;

namespace Gsl.Info.Cadastrais.Tests.Fixtures
{
    public class EnderecoFixture
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public EnderecoFixture() { }

        public EnderecoCompleto CriarEndereco()
        {
            return new EnderecoCompleto("33900788", "Rua Teste", "7", "Casa", "BH", "MG");  
        }       
    }
}
