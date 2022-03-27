using AutoMapper;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Infrastructure.Models;

namespace Gsl.Gestao.Estrategica.Application.Mapping
{
    /// <summary>
    /// Mapper de estoque
    /// </summary>
    public class EstoqueMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public EstoqueMap()
        {
            CreateMap<Estoque, EstoqueModel>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.DepositoCodigo, m => m.MapFrom(src => src.DepositoCodigo))                
                .ForMember(dest => dest.MercadoriaCodigo, m => m.MapFrom(src => src.MercadoriaCodigo))
                .ForMember(dest => dest.ValorTotal, m => m.MapFrom(src => src.ValorTotal))
                .ReverseMap();
        }
    }
}
