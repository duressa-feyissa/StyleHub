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
        public bool IsPublished { get; set; } = false;
        public required string City { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public string? BrandId { get; set; }
        public List<string> CategoryIds { get; set; } = new List<string>();
        public List<string> SizeIds { get; set; } = new List<string>();
        public List<string> ColorIds { get; set; } = new List<string>();
        public List<string> MaterialIds { get; set; } = new List<string>();
    }
}
