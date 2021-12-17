using AutoMapper;
using System;
using System.Linq;

namespace SkyCommerce.Data.Mappers
{
    internal class ProdutoMapperProfile : Profile
    {
        public ProdutoMapperProfile()
        {

            CreateMap<Entities.Produto, Models.Produto>()
                .ForMember(dest => dest.Categorias, opt => opt.MapFrom(m => m.Categorias.Split(";", StringSplitOptions.RemoveEmptyEntries).ToHashSet()))
                .ForMember(dest => dest.Cores, opt => opt.MapFrom(m => m.Cores.Split(";", StringSplitOptions.RemoveEmptyEntries).ToHashSet()))
                .ForMember(dest => dest.Imagens, opt => opt.MapFrom(m => m.Imagens.Split(";", StringSplitOptions.RemoveEmptyEntries).ToHashSet()))
                .ForMember(dest => dest.Avaliacoes, opt => opt.MapFrom(m => m.Avaliacoes.OrderByDescending(b => b.DataAvaliacao)));

            CreateMap<Models.Produto, Entities.Produto>()
                .ForMember(dest => dest.Categorias, opt => opt.MapFrom(m => string.Join(";", m.Categorias)))
                .ForMember(dest => dest.Cores, opt => opt.MapFrom(m => string.Join(";", m.Cores)))
                .ForMember(dest => dest.Imagens, opt => opt.MapFrom(m => string.Join(";", m.Imagens)))
                .ForMember(dest => dest.Avaliacoes, opt => opt.MapFrom(m => m.Avaliacoes));

        }
    }
}