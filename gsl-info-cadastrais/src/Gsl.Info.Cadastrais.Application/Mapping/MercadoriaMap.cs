using AutoMapper;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;

namespace Gsl.Info.Cadastrais.Application.Mapping
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
