using Application.DTO.BrandDTO.DTO;
using Application.DTO.ColorDTO.DTO;
using Application.DTO.MaterialDTO.DTO;
using Application.DTO.ProductDTO.DTO;
using Application.DTO.SizeDTO.DTO;
using Application.DTO.LocationDTO.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, BaseProductDTO>().ReverseMap();
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
			CreateMap<BaseColorDTO, Color>().ReverseMap();
			CreateMap<BaseSizeDTO, Size>().ReverseMap();
			CreateMap<SizeResponseDTO, Size>().ReverseMap();
			CreateMap<BrandResponseDTO, Brand>().ReverseMap();
			CreateMap<BaseBrandDTO, Brand>().ReverseMap();
			CreateMap<MaterialResponseDTO, Material>().ReverseMap();
			CreateMap<BaseMaterialDTO, Material>().ReverseMap();
			CreateMap<LocationResponseDTO, Location>().ReverseMap();
			CreateMap<BaseLocationDTO, Location>().ReverseMap();
		}
	}
}
