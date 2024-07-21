namespace backend.Application.DTO.Shop.ShopDTO.DTO;

public class CreateShopDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Categories { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string PhoneNumber { get; set; }
    public string? Banner { get; set; }
    public string Logo { get; set; }
    public Dictionary<string, string> SocialMediaLinks { get; set; }
    public string? Website { get; set; }
}