using AutoMapper;
using SkyCommerce.Data.Entities;
using Marca = SkyCommerce.Models.Marca;

namespace SkyCommerce.Data.Mappers
{
    internal class MarcaMapperProfile : Profile
    {
        public MarcaMapperProfile()
        {
            CreateMap<Entities.Marca, Marca>().ReverseMap();
        }
    }
}