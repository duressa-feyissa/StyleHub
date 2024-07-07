using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Entities.Product;

public class ProductBrand
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("Product")]
    public required string ProductId { get; set; }

    [Required]
    public  virtual required Brand Brand { get; set; }
}