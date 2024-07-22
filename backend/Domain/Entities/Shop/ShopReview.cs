using System.ComponentModel.DataAnnotations.Schema;
using backend.Domain.Common;

namespace backend.Domain.Entities.Shop;

public class ShopReview: BaseEntity
{
    [ForeignKey("ShopId")]
    public string ShopId { get; set; }
    public Shop Shop { get; set; }
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    public User.User User { get; set; }
    public string Review { get; set; }
    public int Rating { get; set; }
}