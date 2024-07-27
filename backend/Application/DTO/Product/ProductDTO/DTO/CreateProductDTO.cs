namespace backend.Application.DTO.Product.ProductDTO.DTO
{
    public class CreateProductDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required string Status { get; set; } = "draft";
        public string? VideoUrl { get; set; }
        public required string ShopId { get; set; }
        public string Condition { get; set; } = "new";
        public required bool IsNegotiable { get; set; } = false;
        public bool InStock { get; set; } = true;
        public List<string> ImageIds { get; set; } = new List<string>();
        public List<string> CategoryIds { get; set; } = new List<string>();
        public List<string> BrandIds { get; set; } = new List<string>();
        public List<string> DesignIds { get; set; } = new List<string>();
        public List<string> SizeIds { get; set; } = new List<string>();
        public List<string> ColorIds { get; set; } = new List<string>();
        public List<string> MaterialIds { get; set; } = new List<string>();
    }
}
