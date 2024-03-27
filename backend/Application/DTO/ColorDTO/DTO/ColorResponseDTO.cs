
namespace Application.DTO.ColorDTO.DTO
{
    public class ColorResponseDTO : IBaseColorDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string HexCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}