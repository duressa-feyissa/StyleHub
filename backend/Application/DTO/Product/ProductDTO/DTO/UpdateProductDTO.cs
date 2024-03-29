namespace Application.DTO.Product.ProductDTO.DTO
{
    public class UpdateProductDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Target { get; set; }
        public string? Condition { get; set; }
        public string? BrandId { get; set; }
        public List<string>? CategoryIds { get; set; }
        public string? LocationId { get; set; }
        public List<string>? BinaryImages { get; set; }
        public List<string>? SizeIds { get; set; }
        public List<string>? ColorIds { get; set; }
        public List<string>? MaterialIds { get; set; }
        public bool IsNegotiable { get; set; } = false;
    }
}
