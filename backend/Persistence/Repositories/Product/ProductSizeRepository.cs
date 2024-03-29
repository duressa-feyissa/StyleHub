using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories
{
    public class ProductSizeRepository : GenericRepository<ProductSize>, IProductSizeRepository
    {
        StyleHubDBContext _context;

        public ProductSizeRepository(StyleHubDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<ProductSize> GetById(string id)
        {
            var productsize = await _context.ProductSizes.FirstOrDefaultAsync(u => u.Id == id);
            return productsize!;
        }
    }
}
