using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Entities.Shop;

public class WorkingHour
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Shop")]
    public required string ShopId { get; set; }
    public required string Day { get; set; }
    public required string Time { get; set; }
}