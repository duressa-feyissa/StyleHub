
namespace Application.DTO.BrandDTO.DTO
{
    public class BaseBrandDTO : IBaseBrandDTO
    {
        public required string Name { get; set; }
        public required string Logo { get; set; }
        public string Country { get; set; } = string.Empty;
    }
}