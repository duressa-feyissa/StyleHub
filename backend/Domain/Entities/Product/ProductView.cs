using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Entities.Product;

public class ProductView
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Product")]
    public required string ProductId { get; set; }
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public required DateTime ViewedAt { get; set; } = DateTime.Now;
}