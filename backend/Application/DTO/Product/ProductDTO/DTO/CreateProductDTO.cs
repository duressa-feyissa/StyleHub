namespace Application.DTO.Product.ProductDTO.DTO
{
    public class CreateProductDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required string Target { get; set; }
        public int Quantity { get; set; } = 1;
        public string Condition { get; set; } = "new";
        public required bool IsNegotiable { get; set; } = false;
        public required string LocationId { get; set; }
        public required List<string> BinaryImages { get; set; }
        public required List<string> CategoryIds { get; set; }
        public string? BrandId { get; set; }
        public List<string>? SizeIds { get; set; }
        public List<string>? ColorIds { get; set; }
        public List<string>? MaterialIds { get; set; }
    }
}
