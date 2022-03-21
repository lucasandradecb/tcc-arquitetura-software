using AutoMapper;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;

namespace Gsl.Gestao.Estrategica.Application.Mapping
{
    /// <summary>
    /// Mapper de mercadoria
    /// </summary>
    public class MercadoriaMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public MercadoriaMap()
        {
            CreateMap<Mercadoria, MercadoriaModel>()
                .ForMember(dest => dest.Quantidade, m => m.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.Valor, m => m.MapFrom(src => src.Valor))              
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ReverseMap();
        }
    }
}
