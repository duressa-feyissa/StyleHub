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

        public async Task<bool> DeleteByProductId(string productId)
        {
            var productSizes = await _context
                .ProductSizes.Where(u => u.ProductId == productId)
                .ToListAsync();
            _context.ProductSizes.RemoveRange(productSizes);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductSize> GetById(string id)
        {
            var productsize = await _context.ProductSizes.FirstOrDefaultAsync(u => u.Id == id);
            return productsize!;
        }
    }
}
