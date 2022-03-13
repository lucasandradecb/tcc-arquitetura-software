using AutoMapper;

namespace Gsl.Info.Cadastrais.Tests.Fixtures
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new Application.Mapping.UsuarioMap());
                opts.AddProfile(new Application.Mapping.VeiculoMap());
            });

            Mapper = config.CreateMapper();
        }
    }
}
