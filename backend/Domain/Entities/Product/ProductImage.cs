using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Domain.Entities.Common;

namespace backend.Domain.Entities.Product;

public class ProductImage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("Product")]
    public required string ProductId { get; set; }

    [Required]
    public required virtual Image Image { get; set; }
}