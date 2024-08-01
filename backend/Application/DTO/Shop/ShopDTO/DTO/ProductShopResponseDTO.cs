namespace backend.Application.DTO.Shop.ShopDTO.DTO;

public class ProductShopResponseDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public  string Street { get; set; }
    public string SubLocality { get; set; }
    public string SubAdministrativeArea { get; set; }
    public string PostalCode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Logo { get; set; }
    public string PhoneNumber { get; set; }
}