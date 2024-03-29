using Application.DTO.Common.Location.DTO;
using Application.DTO.Product.BrandDTO.DTO;
using Application.DTO.Product.CategoryDTO.DTO;
using Application.DTO.Product.ColorDTO.DTO;
using Application.DTO.Product.MaterialDTO.DTO;
using Application.DTO.Product.ProductDTO.DTO;
using Application.DTO.Product.SizeDTO.DTO;
using AutoMapper;
using Domain.Entities.Common;
using Domain.Entities.Product;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>()
                .ForMember(
                    dest => dest.Colors,
                    opt => opt.MapFrom(src => src.ProductColors.Select(pc => pc.Color))
                )
                .ForMember(
                    dest => dest.Sizes,
                    opt => opt.MapFrom(src => src.ProductSizes.Select(ps => ps.Size))
                )
                .ForMember(
                    dest => dest.Materials,
                    opt => opt.MapFrom(src => src.ProductMaterials.Select(pm => pm.Material))
                )
                .ForMember(
                    dest => dest.Images,
                    opt =>
                        opt.MapFrom(src =>
                            src.Images.Select(pi => new ProductImageResponseDTO
                            {
                                Id = pi.Id,
                                ImageUrl = pi.ImageUrl
                            })
                        )
                )
                .ReverseMap();

            CreateMap<ColorResponseDTO, Color>().ReverseMap();
            CreateMap<CreateColorDTO, Color>().ReverseMap();
            CreateMap<UpdateColorDTO, Color>().ReverseMap();
            CreateMap<CreateSizeDTO, Size>().ReverseMap();
            CreateMap<UpdateSizeDTO, Size>().ReverseMap();
            CreateMap<SizeResponseDTO, Size>().ReverseMap();
            CreateMap<BrandResponseDTO, Brand>().ReverseMap();
            CreateMap<CreateBrandDTO, Brand>().ReverseMap();
            CreateMap<UpdateBrandDTO, Brand>().ReverseMap();
            CreateMap<MaterialResponseDTO, Material>().ReverseMap();
            CreateMap<BaseMaterialDTO, Material>().ReverseMap();
            CreateMap<LocationResponseDTO, Location>().ReverseMap();
            CreateMap<CreateLocationDTO, Location>().ReverseMap();
            CreateMap<UpdateLocationDTO, Location>().ReverseMap();
            CreateMap<CategoryResponseDTO, Category>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
