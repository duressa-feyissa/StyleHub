using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ProductBrandRepository(StyleHubDBContext context)
        : GenericRepository<ProductBrand>(context), IProductBrandRepository
    {
        public async Task<bool> DeleteByProductId(string productId)
        {
            var productbrands = await context.ProductBrands.Where(u => u.ProductId == productId).ToListAsync();
            context.ProductBrands.RemoveRange(productbrands);
            return true;
        }

        public async Task<ProductBrand> GetById(string id)
        {
            var productbrand = await context.ProductBrands.FirstOrDefaultAsync(u => u.Id == id);
            return productbrand;
        }
    }
}