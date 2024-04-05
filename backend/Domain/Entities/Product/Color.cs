using Domain.Common;

namespace Domain.Entities.Product
{
    public class Color : BaseEntity
    {
        public required string Name { get; set; }
        public required string HexCode { get; set; }
    }
}
