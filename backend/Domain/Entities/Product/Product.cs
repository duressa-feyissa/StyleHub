using System.ComponentModel.DataAnnotations;
using backend.Domain.Common;
using backend.Domain.Entities.Common;

namespace backend.Domain.Entities.Product
{
    public class Product : BaseEntity
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required float Price { get; set; }

        [Required]
        public required int Quantity { get; set; }
        
        [Required]
        public required string Condition { get; set; }
        
        [Required]
        public bool IsPublished { get; set; } = false;

        [Required]
        public required string City { get; set; }

        [Required]
        public required double Latitude { get; set; }

        [Required]
        public required double Longitude { get; set; }
        
        public virtual HashSet<ProductDesign> ProductDesigns { get; set; } = new HashSet<ProductDesign>();
        
        public virtual HashSet<ProductBrand> ProductBrands { get; set; } = new HashSet<ProductBrand>();
        public virtual HashSet<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
        public virtual HashSet<ProductSize> ProductSizes { get; set; } = new HashSet<ProductSize>();
        public virtual HashSet<ProductColor> ProductColors { get; set; } =
            new HashSet<ProductColor>();
        public virtual HashSet<ProductMaterial> ProductMaterials { get; set; } =
            new HashSet<ProductMaterial>();
        public virtual HashSet<ProductCategory> ProductCategories { get; set; } =
            new HashSet<ProductCategory>();
        public required bool IsNegotiable { get; set; } = false;
    }
}
