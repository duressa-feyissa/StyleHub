using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;

namespace backend.Persistence.Repositories.Product;

public class ProductViewRepository(StyleHubDBContext context)
    : GenericRepository<ProductView>(context), IProductViewRepository
{
    
}