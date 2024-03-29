namespace Application.DTO.Product.MaterialDTO.DTO
{
    public class MaterialResponseDTO : IBaseMaterialDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}