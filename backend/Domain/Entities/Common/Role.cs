using Domain.Common;

namespace Domain.Entities.Common
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Code { get; set; }
    }
}
