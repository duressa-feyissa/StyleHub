using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
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
