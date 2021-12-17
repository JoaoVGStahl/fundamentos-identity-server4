using AutoMapper;
using SkyCommerce.Models;

namespace SkyCommerce.Data.Mappers
{
    internal class AvaliacaoMapperProfile : Profile
    {
        public AvaliacaoMapperProfile()
        {
            CreateMap<Entities.Avaliacao, Avaliacao>()
                .ForMember(dest => dest.ProdutoUrl, opt => opt.MapFrom(m => m.Produto.NomeUnico));

            CreateMap<Models.Avaliacao, Entities.Avaliacao>();
        }
    }
}