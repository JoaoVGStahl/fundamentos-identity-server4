using AutoMapper;

namespace SkyCommerce.Data.Mappers
{
    internal class PedidoMapperProfile : Profile
    {
        public PedidoMapperProfile()
        {

            CreateMap<Models.Pedido, Entities.Pedido>();

            CreateMap<Entities.Pedido, Models.Pedido>()
                .ForMember(dest => dest.Produtos, opt => opt.MapFrom(o => o.Produtos));

            CreateMap<Entities.ProdutoVendido, Models.SnapshotProduto>().ReverseMap();
        }
    }
}