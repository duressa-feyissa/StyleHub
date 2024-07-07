using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ProductDesignRepository(StyleHubDBContext context)
        : GenericRepository<ProductDesign>(context), IProductDesignRepository
    {
        public async Task<bool> DeleteByProductId(string productId)
        {
            var productdesigns = await context.ProductDesigns.Where(u => u.ProductId == productId).ToListAsync();
            context.ProductDesigns.RemoveRange(productdesigns);
            return true;
        }

        public async Task<ProductDesign> GetById(string id)
        {
            var productdesign = await context.ProductDesigns.FirstOrDefaultAsync(u => u.Id == id);
            return productdesign;
        }
    }
}