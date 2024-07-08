using System.ComponentModel.DataAnnotations.Schema;
using backend.Domain.Common;

namespace backend.Domain.Entities.Product;

public class ContactedProduct: BaseEntity
{
    [ForeignKey("Product")]
    public required string ProductId { get; set; }
    [ForeignKey("User")]
    public required string UserId { get; set; }
    public required double Offer { get; set; } = 0;
}