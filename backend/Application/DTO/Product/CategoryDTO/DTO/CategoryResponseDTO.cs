namespace Application.DTO.Product.CategoryDTO.DTO
{
    public class CategoryResponseDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}