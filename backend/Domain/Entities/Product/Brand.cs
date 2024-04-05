using Domain.Common;

namespace Domain.Entities.Product
{
    public class Brand : BaseEntity
    {
        public required string Name { get; set; }
        public required string Logo { get; set; }
        public string Country { get; set; } = string.Empty;
    }
}
