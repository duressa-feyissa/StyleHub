using Application.DTO.Common.Image.DTO;
using Application.DTO.Common.Location.DTO;
using Application.DTO.Product.BrandDTO.DTO;
using Application.DTO.Product.CategoryDTO.DTO;
using Application.DTO.Product.ColorDTO.DTO;
using Application.DTO.Product.MaterialDTO.DTO;
using Application.DTO.Product.SizeDTO.DTO;
using Application.DTO.User.UserDTO.DTO;

namespace Application.DTO.Product.ProductDTO.DTO
{
    public class ProductResponseDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required int Quantity { get; set; }
        public required string Target { get; set; }
        public required string Condition { get; set; }
        public required string City { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public required BrandResponseDTO Brand { get; set; }
        public required HashSet<SizeResponseDTO> Sizes { get; set; }
        public required HashSet<ColorResponseDTO> Colors { get; set; }
        public required HashSet<MaterialResponseDTO> Materials { get; set; }
        public required HashSet<CategoryResponseDTO> Categories { get; set; }
        public required HashSet<ImageResponseDTO> Images { get; set; }
        public required UserSharedResponseDTO User { get; set; }
        public required bool IsNegotiable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
