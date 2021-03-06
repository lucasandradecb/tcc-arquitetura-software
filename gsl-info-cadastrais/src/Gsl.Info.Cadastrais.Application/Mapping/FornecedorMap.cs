using AutoMapper;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.ValueObjects;

namespace Gsl.Info.Cadastrais.Application.Mapping
{
    /// <summary>
    /// Mapper de fornecedor
    /// </summary>
    public class FornecedorMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public FornecedorMap()
        {
            CreateMap<Fornecedor, FornecedorModel>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dest => dest.Latitude, m => m.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, m => m.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Endereco, m => m.MapFrom(src => src.Endereco))                
                .ForMember(dest => dest.Cnpj, m => m.MapFrom(src => src.Cnpj))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome));

            CreateMap<EnderecoCompleto, DadosEnderecoModel>()
                .ForMember(dest => dest.Cep, m => m.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Logradouro, m => m.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Numero, m => m.MapFrom(src => src.Numero))
                .ReverseMap();

            CreateMap<FornecedorModel, Fornecedor>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dest => dest.Latitude, m => m.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, m => m.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Endereco, m => m.Ignore())
                .ForMember(dest => dest.Cnpj, m => m.MapFrom(src => src.Cnpj))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ConstructUsing(src =>
                    new Fornecedor(
                        src.Nome,
                        src.Cnpj,
                        new EnderecoCompleto(src.Endereco.Cep, src.Endereco.Logradouro, src.Endereco.Numero, src.Endereco.Complemento, src.Endereco.Cidade, src.Endereco.Estado),
                        src.Latitude,
                        src.Longitude
                    ));
        }
    }
}
