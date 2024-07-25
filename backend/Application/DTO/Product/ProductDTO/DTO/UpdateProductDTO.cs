namespace backend.Application.DTO.Product.ProductDTO.DTO
{
    public class UpdateProductDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public bool? InStock { get; set; }
        public string? Condition { get; set; }
        public string? VideoUrl { get; set; }
        public bool? IsNegotiable { get; set; }
        public string? Status { get; set; }
        public List<string>? ImageIds { get; set; }
        public List<string>? CategoryIds { get; set; }
        public List<string>? DesignIds { get; set; }
        public List<string>? BrandIds { get; set; }
        public List<string>? SizeIds { get; set; }
        public List<string>? ColorIds { get; set; }
        public List<string>? MaterialIds { get; set; }
    }
}
