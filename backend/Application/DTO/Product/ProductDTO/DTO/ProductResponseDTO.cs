using Domain.Entities.Common;
using Domain.Entities.Product;

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
		public required Brand Brand { get; set; }
		public required Location Location { get; set; }
		public required HashSet<Size> Sizes { get; set; }
		public required HashSet<Color> Colors { get; set; }
		public required HashSet<Material> Materials { get; set; }
		public required HashSet<Category> Categories { get; set; }
		public required HashSet<ProductImageResponseDTO> Images { get; set; }
		public required bool IsNegotiable { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
