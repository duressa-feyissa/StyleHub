using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Product
{
    public class ProductImage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public required string ProductId { get; set; }
        [Required]
        public required string ImageUrl { get; set; }
    }
}
