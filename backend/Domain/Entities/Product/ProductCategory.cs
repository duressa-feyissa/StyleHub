using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Product
{
    public class ProductCategory
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Product")]
        public required string ProductId { get; set; }

        [Required]
        public virtual required Category Category { get; set; }
    }
}
