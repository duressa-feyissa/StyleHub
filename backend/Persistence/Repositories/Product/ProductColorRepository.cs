using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ProductColorRepository(StyleHubDBContext context)
        : GenericRepository<ProductColor>(context), IProductColorRepository
    {
        public async Task<bool> DeleteByProductId(string productId)
        {
            var productcolors = await context.ProductColors.Where(u => u.ProductId == productId).ToListAsync();
            context.ProductColors.RemoveRange(productcolors);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductColor> GetById(string id)
        {
            var productcolor = await context.ProductColors.FirstOrDefaultAsync(u => u.Id == id);
            return productcolor!;
        }
    }
}