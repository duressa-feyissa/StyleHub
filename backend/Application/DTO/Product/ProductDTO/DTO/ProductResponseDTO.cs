using backend.Application.DTO.Common.Image.DTO;
using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.DTO.User.UserDTO.DTO;

namespace backend.Application.DTO.Product.ProductDTO.DTO
{
    public class ProductResponseDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required string Status { get; set; }
        public required bool InStock { get; set; }
        public required string Condition { get; set; }
        public string? VideoUrl { get; set; }
        public required bool IsFavorite { get; set; } = false;
        public required HashSet<SizeResponseDTO> Sizes { get; set; }
        public required HashSet<ColorResponseDTO> Colors { get; set; }
        public required HashSet<MaterialResponseDTO> Materials { get; set; }
        public required HashSet<CategoryResponseDTO> Categories { get; set; }
        public required HashSet<ImageResponseDTO> Images { get; set; }
        public required HashSet<BrandResponseDTO> Brands { get; set; }
        public required HashSet<DesignResponseDTO> Designs { get; set; }
        public required ProductShopResponseDTO Shop { get; set; }
        public required bool IsNegotiable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
