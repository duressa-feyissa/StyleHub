using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Domain.Common;

namespace backend.Domain.Entities.Shop;

public class Shop: BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required string Street { get; set; }
    public required string SubLocality { get; set; }
    public required string SubAdministrativeArea { get; set; }
    public required string PostalCode { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Banner { get; set; }
    public string Logo { get; set; } = string.Empty;
    public string SocialMedias { get; set; } = string.Empty;
    public bool Verified { get; set; } = false;
    public bool Active { get; set; } = false;
    public DateTime LastSeenAt { get; set; } = DateTime.Now;
    public string? Website { get; set; }
    public required User.User Owner { get; set; }
    [Required]
    [ForeignKey(nameof(Owner))]
    public required string UserId { get; set; }
    public string OwnerIdentityCardUrl { get; set; } = string.Empty;
    public string BusinessRegistrationNumber { get; set; } = string.Empty;
    public string BusinessRegistrationDocumentUrl { get; set; } = string.Empty;
    public DateTime? VerifiedAt { get; set; }
    public virtual HashSet<ShopReview> ShopReviews { get; set; } = new HashSet<ShopReview>();
    public virtual HashSet<WorkingHour> WorkingHours { get; set; } = new HashSet<WorkingHour>();
    public virtual HashSet<Product.Product> Products { get; set; } = new HashSet<Product.Product>();
}