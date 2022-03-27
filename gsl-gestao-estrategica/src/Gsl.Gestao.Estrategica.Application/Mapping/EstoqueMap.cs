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
                .ForMember(dest => dest.ListaMercadorias, m => m.MapFrom(src => src.ListaMercadorias))
                .ReverseMap();

            CreateMap<Mercadoria, MercadoriaGatewayModel>()
                .ForMember(dest => dest.Quantidade, m => m.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.Valor, m => m.MapFrom(src => src.Valor))
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.FornecedorId, m => m.MapFrom(src => src.FornecedorId))
                .ReverseMap();
        }
    }
}
