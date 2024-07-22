using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Entities.Shop;
public class ShopFollow
{ 
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Shop")]
    public required string ShopId { get; set; }
    public Shop Shop { get; set; }
    [ForeignKey("User")]
    public required string UserId { get; set; }
    public  required DateTime FollowedAt { get; set; } = DateTime.Now;
}

