using Application.DTO.ProductDTO.DTO;

namespace Application.DTO.ProductDTO.DTO
{
    public class BaseProductDTO : IBaseProductDTO
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price { get; set; }
    }
}