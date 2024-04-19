namespace backend.Application.DTO.Product.BrandDTO.DTO
{
    public class BrandResponseDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Logo { get; set; }
        public string Country { get; set; } = string.Empty;
    }
}
