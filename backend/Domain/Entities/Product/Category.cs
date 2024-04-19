using backend.Domain.Common;

namespace backend.Domain.Entities.Product
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public required string Image { get; set; }
    }
}
