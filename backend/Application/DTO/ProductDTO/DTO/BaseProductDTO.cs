using Application.DTO.ProductDTO.DTO;

namespace Application.DTO.ProductDTO.DTO
{
	public class BaseProductDTO : IBaseProductDTO
	{
		public required string Title { get; set; }
		public required string Description { get; set; }
		public required float Price { get; set; }
		public required int Quantity { get; set; }
		public required string Target { get; set; }
		public required string Condition { get; set; }
		public required string BrandId { get; set; }
		public required string LocationId { get; set; }
		public required List<string> BinaryImages { get; set; }
		public required List<string> SizeIds { get; set; }
		public required List<string> ColorIds { get; set; }
		public required List<string> MaterialIds { get; set; }
		public required bool IsNegotiable { get; set; } = false;
	}
}