
namespace Application.DTO.BrandDTO.DTO
{
    public class BrandResponseDTO : IBaseBrandDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Logo { get; set; }
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}