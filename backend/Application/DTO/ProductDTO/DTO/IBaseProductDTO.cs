namespace Application.DTO.ProductDTO.DTO
{
    public interface IBaseProductDTO
    {
        string Title { get; set; }
        string Description { get; set; }
        float Price { get; set; }
    }
}