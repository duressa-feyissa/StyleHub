
using Domain.Common;

namespace Domain.Entities.Product
{
    public class Size : BaseEntity
    {
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
    }
}
