using AutoMapper;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;

namespace Gsl.Gestao.Estrategica.Application.Mapping
{
    /// <summary>
    /// Mapper de pedido
    /// </summary>
    public class PedidoMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public PedidoMap()
        {
            CreateMap<Pedido, PedidoModel>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.ClienteCpf, m => m.MapFrom(src => src.ClienteCpf))                
                .ForMember(dest => dest.ItensPedido, m => m.MapFrom(src => src.ItensPedido))
                .ForMember(dest => dest.ValorTotal, m => m.MapFrom(src => src.ValorTotal))
                .ReverseMap();

            CreateMap<ItemPedido, ItemPedidoModel>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Valor, m => m.MapFrom(src => src.Valor))
                .ForMember(dest => dest.MercadoriaCodigo, m => m.MapFrom(src => src.MercadoriaCodigo))
                .ForMember(dest => dest.MercadoriaQuantidade, m => m.MapFrom(src => src.MercadoriaQuantidade))
                .ReverseMap();
        }
    }
}
