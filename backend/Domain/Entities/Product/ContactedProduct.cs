using System.ComponentModel.DataAnnotations.Schema;
using backend.Domain.Common;

namespace backend.Domain.Entities.Product;

public class ContactedProduct
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Product")]
    public required string ProductId { get; set; }
    public Product Product { get; set; }
    [ForeignKey("User")] public required string UserId { get; set; }
    public DateTime ContactedAt { get; set; } = DateTime.Now;
}