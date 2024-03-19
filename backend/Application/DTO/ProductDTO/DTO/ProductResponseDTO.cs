
namespace Application.DTO.ProductDTO.DTO
{
    public class ProductResponseDTO : IBaseProductDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}