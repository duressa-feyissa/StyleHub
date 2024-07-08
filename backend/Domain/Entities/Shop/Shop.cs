using backend.Domain.Common;

namespace backend.Domain.Entities.Shop;

public class Shop: BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required WorkingHour WorkingHour { get; set; }
    public string? Banner { get; set; }
    public string? Logo { get; set; }
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string StreetAddress { get; set; }
    public required string ZipCode { get; set; }
    public required string Latitude { get; set; }
    public required string Longitude { get; set; }
    public required string PhoneNumber { get; set; }
    public required string SocialMediaLinks { get; set; }
    public required bool Verified { get; set; } = false;
    public required bool Active { get; set; } = false;
    public required DateTime LastSeenAt { get; set; } = DateTime.Now;
    public string? Website { get; set; }
}