using backend.Domain.Common;

namespace backend.Domain.Entities.Product
{
    public class Material : BaseEntity
    {
        public required string Name { get; set; }
    }
}
