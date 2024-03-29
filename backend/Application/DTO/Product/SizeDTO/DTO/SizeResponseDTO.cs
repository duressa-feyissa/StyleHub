namespace Application.DTO.Product.SizeDTO.DTO
{
    public class SizeResponseDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
