using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required float Price { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required string Target { get; set; }

        [Required]
        public required string Condition { get; set; }

        [Required]
        public virtual required Brand Brand { get; set; }

        [Required]
        public virtual required Location Location { get; set; }
        public virtual HashSet<ProductImage> Images { get; set; } = new HashSet<ProductImage>();
        public virtual HashSet<ProductSize> ProductSizes { get; set; } = new HashSet<ProductSize>();
        public virtual HashSet<ProductColor> ProductColors { get; set; } =
            new HashSet<ProductColor>();
        public virtual HashSet<ProductMaterial> ProductMaterials { get; set; } =
            new HashSet<ProductMaterial>();
        public required bool IsNegotiable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
