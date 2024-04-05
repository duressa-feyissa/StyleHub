using Domain.Common;

namespace Domain.Entities.Product
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public required string Image { get; set; }
    }
}
