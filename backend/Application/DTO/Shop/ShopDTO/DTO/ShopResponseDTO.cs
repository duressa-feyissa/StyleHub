namespace backend.Application.DTO.Shop.ShopDTO.DTO;

public class ShopResponseDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Categories { get; set; }
    public double Rating { get; set; }
    public  string Street { get; set; }
    public string SubLocality { get; set; }
    public string SubAdministrativeArea { get; set; }
    public string PostalCode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string PhoneNumber { get; set; }
    public string? Banner { get; set; }
    public string Logo { get; set; }
    public Dictionary<string, string> SocialMediaLinks { get; set; }
    public bool Verified { get; set; }
    public bool Active { get; set; }
    public DateTime LastSeenAt { get; set; }
    public string? Website { get; set; }
    public string UserId { get; set; }
}
