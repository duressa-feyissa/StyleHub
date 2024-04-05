using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities.Common
{
    public class Image : BaseEntity
    {
        [Required]
        public required string ImageUrl { get; set; }

        [Required]
        public required User.User User { get; set; }

        public Product.Product? Product { get; set; }
    }
}
