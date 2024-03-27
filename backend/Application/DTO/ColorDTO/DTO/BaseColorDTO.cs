
namespace Application.DTO.ColorDTO.DTO
{
    public class BaseColorDTO : IBaseColorDTO
    {
        public required string Name { get; set; }
        public required string HexCode { get; set; }

    }
}