namespace Application.DTO.ProductDTO.DTO
{
    public interface IBaseProductDTO
    {
        string Title { get; set; }
        string Description { get; set; }
        float Price { get; set; }
        int Quantity { get; set; }
        string Target { get; set; }
        string Condition { get; set; }
        bool IsNegotiable { get; set; }
    }
}