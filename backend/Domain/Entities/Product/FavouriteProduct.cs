using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Entities.Product;

public class FavouriteProduct
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Product")]
    public string ProductId { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual Product Product { get; set; }
    public virtual User.User User { get; set; }
}