using Application.DTO.Common.Image.DTO;
using Application.DTO.Common.Location.DTO;
using Application.DTO.Common.Role.DTO;
using Application.DTO.Product.BrandDTO.DTO;
using Application.DTO.Product.CategoryDTO.DTO;
using Application.DTO.Product.ColorDTO.DTO;
using Application.DTO.Product.MaterialDTO.DTO;
using Application.DTO.Product.ProductDTO.DTO;
using Application.DTO.Product.SizeDTO.DTO;
using Application.DTO.User.AuthenticationDTO.DTO;
using Application.DTO.User.AuthenticationDTO.Validations;
using Application.DTO.User.UserDTO.DTO;
using AutoMapper;
using Domain.Entities.Common;
using Domain.Entities.Product;
using Domain.Entities.User;
using Infrastructure.Repository;

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
					dest => dest.Brand,
					opt =>
						opt.MapFrom(src => new BrandResponseDTO
						{
							Id = src.Brand.Id,
							Name = src.Brand.Name,
							Logo = src.Brand.Logo
						})
				)
				.ForMember(
					dest => dest.User,
					opt =>
						opt.MapFrom(src => new UserSharedResponseDTO
						{
							Id = src.User.Id,
							FirstName = src.User.FirstName,
							LastName = src.User.LastName,
							PhoneNumber = src.User.PhoneNumber,
							Email = src.User.Email,
							Latitude = src.User.Longitude,
							Longitude = src.User.Longitude,
							ProfilePicture = src.User.ProfilePicture,
							Country = src.User.Country,
							City = src.User.City,
							Address = src.User.Address
						})
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
				)
				.ForMember(
					dest => dest.Images,
					opt =>
						opt.MapFrom(src =>
							src.Images.Select(pi => new ImageResponseDTO
							{
								Id = pi.Id,
								ImageUrl = pi.ImageUrl
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
			CreateMap<ImageRepository, Image>().ReverseMap();
			CreateMap<UserSharedResponseDTO, User>().ReverseMap();
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
