using backend.Domain.Common;

namespace backend.Domain.Entities.Common
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Code { get; set; }
    }
}
