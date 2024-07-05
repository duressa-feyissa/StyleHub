using backend.Domain.Common;

namespace backend.Domain.Entities.Product;

public class Design  : BaseEntity
{
    public required string Name { get; set; }
}