using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ProductSizeRepository(StyleHubDBContext context)
        : GenericRepository<ProductSize>(context), IProductSizeRepository
    {
        public async Task<bool> DeleteByProductId(string productId)
        {
            var productSizes = await context
                .ProductSizes.Where(u => u.ProductId == productId)
                .ToListAsync();
            context.ProductSizes.RemoveRange(productSizes);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductSize> GetById(string id)
        {
            var productsize = await context.ProductSizes.FirstOrDefaultAsync(u => u.Id == id);
            return productsize!;
        }
    }
}
