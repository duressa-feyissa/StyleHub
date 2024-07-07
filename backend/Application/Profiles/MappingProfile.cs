using AutoMapper;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.DTO.Common.Location.DTO;
using backend.Application.DTO.Common.Role.DTO;
using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.DTO.User.UserDTO.DTO;
using backend.Domain.Entities.Common;
using backend.Domain.Entities.Product;
using backend.Domain.Entities.User;
using backend.Infrastructure.Repository;

namespace backend.Application.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, CreateProductDTO>().ReverseMap();
			CreateMap<Product, UpdateProductDTO>().ReverseMap();
			CreateMap<Product, ProductResponseDTO>()
				.ForMember(
					dest => dest.Brands,
					opt =>
						opt.MapFrom(src =>
							src.ProductBrands.Select(pc => new BrandResponseDTO
								{
									Id = pc.Brand.Id,
									Name = pc.Brand.Name,
									Logo = pc.Brand.Logo
								})
								.ToList()
						)
				)
				.ForMember(
					dest => dest.Designs,
					opt =>
						opt.MapFrom(src =>
							src.ProductDesigns.Select(pc => new DesignResponseDTO
								{
									Id = pc.Design.Id,
									Name = pc.Design.Name,
								})
								.ToList()
						)
				)
				.ForMember(
					dest => dest.Categories,
					opt =>
						opt.MapFrom(src =>
							src.ProductCategories.Select(pc => new CategoryResponseDTO
							{
								Id = pc.Category.Id,
								Name = pc.Category.Name,
								Image = pc.Category.Image
							})
								.ToList()
						)
				)
				.ForMember(
					dest => dest.Colors,
					opt =>
						opt.MapFrom(src =>
							src.ProductColors.Select(pc => new ColorResponseDTO
							{
								Id = pc.Color.Id,
								Name = pc.Color.Name,
								HexCode = pc.Color.HexCode
							})
								.ToList()
						)
				)
				.ForMember(
					dest => dest.Sizes,
					opt =>
						opt.MapFrom(src =>
							src.ProductSizes.Select(ps => new SizeResponseDTO
							{
								Id = ps.Size.Id,
								Name = ps.Size.Name,
								Abbreviation = ps.Size.Abbreviation
							})
								.ToList()
						)
				)
				.ForMember(
					dest => dest.Materials,
					opt =>
						opt.MapFrom(src =>
							src.ProductMaterials.Select(pm => new MaterialResponseDTO
							{
								Id = pm.Material.Id,
								Name = pm.Material.Name
							})
								.ToList()
						)
				).ForMember(
					dest => dest.Images,
					opt =>
						opt.MapFrom(src =>
							src.ProductImages.Select(pm => new ImageResponseDTO
								{
									Id = pm.Image.Id,
									ImageUrl = pm.Image.ImageUrl
								})
								.ToList()
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
			CreateMap<RoleResponseDTO, Role>().ReverseMap();
			CreateMap<CreateRoleDTO, Role>().ReverseMap();
			CreateMap<UpdateRoleDTO, Role>().ReverseMap();
			CreateMap<RegisterationRequestDTO, User>().ReverseMap();
			CreateMap<AuthenticationResponseDTO, User>().ReverseMap();
			CreateMap<LoginRequestDTO, User>().ReverseMap();
			CreateMap<RegisterationResponseDTO, User>().ReverseMap();
			CreateMap<ImageResponseDTO, Image>().ReverseMap();
			CreateMap<UserSharedResponseDTO, User>().ReverseMap();
			CreateMap<DesignResponseDTO, Design>().ReverseMap();
			CreateMap<BaseDesignDTO, Design>().ReverseMap();
			CreateMap<UserResponseDTO, User>().ReverseMap()
				.ForMember(
					dest => dest.Role,
					opt =>
						opt.MapFrom(src => new Role
						{
							Id = src.Role.Id,
							Name = src.Role.Name,
							Description = src.Role.Description,
							Code = src.Role.Code
						})
				);
		}
	}
}
