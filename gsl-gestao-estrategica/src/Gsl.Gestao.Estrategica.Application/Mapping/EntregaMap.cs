using AutoMapper;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Domain.ValueObjects;
using Gsl.Gestao.Estrategica.Infrastructure.Models;

namespace Gsl.Gestao.Estrategica.Application.Mapping
{
    /// <summary>
    /// Mapper de entrega
    /// </summary>
    public class EntregaMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public EntregaMap()
        {
            CreateMap<Entrega, EntregaModel>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.LatitudeEntrega, m => m.MapFrom(src => src.LatitudeEntrega))  
                .ForMember(dest => dest.LongitudeEntrega, m => m.MapFrom(src => src.LongitudeEntrega))
                .ForMember(dest => dest.StatusEntrega, m => m.MapFrom(src => src.StatusEntrega))
                .ForMember(dest => dest.PedidoId, m => m.MapFrom(src => src.PedidoId))
                .ReverseMap();
        }
    }
}
