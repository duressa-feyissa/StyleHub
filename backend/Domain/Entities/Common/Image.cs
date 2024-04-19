using System.ComponentModel.DataAnnotations;
using backend.Domain.Common;

namespace backend.Domain.Entities.Common
{
    public class Image : BaseEntity
    {
        [Required]
        public required string ImageUrl { get; set; }

        [Required]
        public required User.User User { get; set; }
    }
}
