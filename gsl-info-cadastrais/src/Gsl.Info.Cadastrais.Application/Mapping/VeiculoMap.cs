using AutoMapper;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;

namespace Gsl.Info.Cadastrais.Application.Mapping
{
    /// <summary>
    /// Mapper de veiculo
    /// </summary>
    public class VeiculoMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public VeiculoMap()
        {
            CreateMap<MarcaVeiculoModel, MarcaVeiculo>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Descricao, m => m.MapFrom(src => src.Descricao))
                .ReverseMap();

            CreateMap<ModeloVeiculoModel, ModeloVeiculo>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Descricao, m => m.MapFrom(src => src.Descricao))
                .ReverseMap();

            CreateMap<VeiculoModel, Veiculo>()
                .ForMember(dest => dest.Ano, m => m.MapFrom(src => src.Ano))
                .ForMember(dest => dest.Categoria, m => m.MapFrom(src => src.Categoria))
                .ForMember(dest => dest.CodigoMarca, m => m.MapFrom(src => src.Marca))
                .ForMember(dest => dest.CodigoModelo, m => m.MapFrom(src => src.Modelo))
                .ForMember(dest => dest.Combustivel, m => m.MapFrom(src => src.Combustivel))
                .ForMember(dest => dest.LimitePortaMalas, m => m.MapFrom(src => src.LimitePortaMalas))
                .ForMember(dest => dest.Placa, m => m.MapFrom(src => src.Placa))
                .ReverseMap();

            CreateMap<Agendamento, AgendamentoOutputModel>()
                .ForMember(dest => dest.Categoria, m => m.MapFrom(src => src.CategoriaVeiculo))
                .ForMember(dest => dest.CodigoReserva, m => m.MapFrom(src => src.CodigoReserva))
                .ForMember(dest => dest.Placa, m => m.MapFrom(src => src.Placa))
                .ForMember(dest => dest.ValorHora, m => m.MapFrom(src => src.ValorHoraVeiculo))
                .ForMember(dest => dest.ValorTotal, m => m.MapFrom(src => src.ValorTotalAluguel));

            CreateMap<Veiculo, VeiculoCompletoOutputModel>()
                .ForMember(dest => dest.Ano, m => m.MapFrom(src => src.Ano))
                .ForMember(dest => dest.Categoria, m => m.MapFrom(src => src.Categoria))
                .ForMember(dest => dest.Marca, m => m.MapFrom(src => src.CodigoMarca))
                .ForMember(dest => dest.Modelo, m => m.MapFrom(src => src.CodigoModelo))
                .ForMember(dest => dest.Combustivel, m => m.MapFrom(src => src.Combustivel))
                .ForMember(dest => dest.LimitePortaMalas, m => m.MapFrom(src => src.LimitePortaMalas))
                .ForMember(dest => dest.Placa, m => m.MapFrom(src => src.Placa));
        }
    }
}
