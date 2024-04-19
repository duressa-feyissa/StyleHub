using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ProductCategoryRepository(StyleHubDBContext context) : GenericRepository<ProductCategory>(context),
        IProductCategoryRepository
    {
        public async Task<bool> DeleteByProductId(string productId)
        {
            var productCategories = await context
                .ProductCategories.Where(u => u.ProductId == productId)
                .ToListAsync();
            context.ProductCategories.RemoveRange(productCategories);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductCategory> GetById(string id)
        {
            var productcolor = await context.ProductCategories.FirstOrDefaultAsync(u =>
                u.Id == id
            );
            return productcolor!;
        }
    }
}
