using Domain.Entities;

namespace Application.DTO.ProductDTO.DTO
{
    public class ProductResponseDTO : IBaseProductDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required int Quantity { get; set; }
        public required string Target { get; set; }
        public required string Condition { get; set; }
        public required Brand Brand { get; set; }
        public required Location Location { get; set; }
        public required HashSet<Size> Sizes { get; set; }
        public required HashSet<Color> Colors { get; set; }
        public required HashSet<Material> Materials { get; set; }
        public required HashSet<ProductImageResponseDTO> Images { get; set; }
        public required bool IsNegotiable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
