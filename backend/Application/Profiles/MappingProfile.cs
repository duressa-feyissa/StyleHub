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
using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.DTO.User.UserDTO.DTO;
using backend.Domain.Entities.Common;
using backend.Domain.Entities.Product;
using backend.Domain.Entities.Shop;
using backend.Domain.Entities.User;
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
					dest => dest.Shop,
					opt =>
						opt.MapFrom(src => new ProductShopResponseDTO
						{
							Id = src.Shop.Id,
							Name = src.Shop.Name,
							Street = src.Shop.Street,
							SubLocality = src.Shop.SubLocality,
							SubAdministrativeArea = src.Shop.SubAdministrativeArea,
							PostalCode = src.Shop.PostalCode,
							Latitude = src.Shop.Latitude,
							Longitude = src.Shop.Longitude,
							PhoneNumber = src.Shop.PhoneNumber,
							Logo = src.Shop.Logo
						})
				)
				.ForMember(
					dest => dest.Brands,
					opt =>
						opt.MapFrom(src =>
							src.ProductBrands.Select(pc => new BrandResponseDTO
								{
									Id = pc.Brand.Id,
									Name = pc.Brand.Name,
									Logo = pc.Brand.Logo,
									Country = pc.Brand.Country
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
			CreateMap<ShopResponseDTO, Shop>().ReverseMap();
			CreateMap<CreateShopDTO, Shop>()
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => string.Join(", ", src.Categories)))
				.ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => string.Join(", ", src.SocialMediaLinks.Select(kv => kv.Key + ":" + kv.Value))));
			CreateMap<UpdateShopDTO, Shop>().ReverseMap();
			CreateMap<WorkingHourResponseDTO, WorkingHour>().ReverseMap();
			CreateMap<CreateWorkingHourDTO, WorkingHour>().ReverseMap();
			CreateMap<UpdateWorkingHourDTO, WorkingHour>().ReverseMap();
			CreateMap<ReviewResponseDTO, ShopReview>()
				.ForMember(dest => dest.Review, opt => opt.MapFrom(src => src.Review))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
				.ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.ShopId))
				.ForMember(dest => dest.UserId, opt => opt.Ignore()) // Assuming UserId will be set separately
				.ReverseMap()
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
				.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.User.ProfilePicture))
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
				.ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.ShopId));
				
			CreateMap<CreateReviewDTO, ShopReview>().ReverseMap();
			CreateMap<UpdateReviewDTO, ShopReview>().ReverseMap();
		}
	}
}
