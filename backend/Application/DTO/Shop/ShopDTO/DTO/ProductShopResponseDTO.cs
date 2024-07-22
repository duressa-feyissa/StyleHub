namespace backend.Application.DTO.Shop.ShopDTO.DTO;

public class ProductShopResponseDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Logo { get; set; }
}