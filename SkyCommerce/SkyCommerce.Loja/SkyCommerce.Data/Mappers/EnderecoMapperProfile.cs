using AutoMapper;
using SkyCommerce.Models;

namespace SkyCommerce.Data.Mappers
{
    internal class EnderecoMapperProfile : Profile
    {
        public EnderecoMapperProfile()
        {
            CreateMap<Entities.Endereco, Endereco>().ReverseMap();
        }
    }
}