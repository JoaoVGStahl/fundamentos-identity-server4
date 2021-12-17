using AutoMapper;

namespace SkyCommerce.Data.Mappers
{
    internal class CategoriaMapperProfile : Profile
    {
        public CategoriaMapperProfile()
        {
            CreateMap<Entities.Categoria, Models.Categoria>().ReverseMap();
        }
    }
}