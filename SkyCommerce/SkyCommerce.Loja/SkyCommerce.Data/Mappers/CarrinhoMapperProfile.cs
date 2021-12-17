using AutoMapper;
using Carrinho = SkyCommerce.Models.Carrinho;

namespace SkyCommerce.Data.Mappers
{
    internal class CarrinhoMapperProfile : Profile
    {
        public CarrinhoMapperProfile()
        {
            CreateMap<Entities.Carrinho, Carrinho>()
                .ForMember(m => m.Items, opt => opt.MapFrom(o => o.CarrinhoProdutos));

            CreateMap<Models.Carrinho, Entities.Carrinho>()
                .ForMember(m => m.CarrinhoProdutos, opt => opt.MapFrom(o => o.Items));

            CreateMap<Models.ItemCarrinho, Entities.ItemCarrinho>()
                .ForMember(m => m.NomeProduto, opt => opt.MapFrom(m => m.Produto))
                .ReverseMap();

        }
    }
}