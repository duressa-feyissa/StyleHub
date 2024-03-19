using Application.DTO.ProductDTO.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductResponseDTO, Product>().ReverseMap();
            CreateMap<BaseProductDTO, Product>().ReverseMap();
        }
    }
}