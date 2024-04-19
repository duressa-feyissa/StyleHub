using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ProductMaterialRepository(StyleHubDBContext context)
        : GenericRepository<ProductMaterial>(context), IProductMaterialRepository
    {
        public async Task<bool> DeleteByProductId(string productId)
        {
            var productmaterials = await context.ProductMaterials.Where(u => u.ProductId == productId).ToListAsync();
            context.ProductMaterials.RemoveRange(productmaterials);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductMaterial> GetById(string id)
        {
            var productmaterial = await context.ProductMaterials.FirstOrDefaultAsync(u => u.Id == id);
            return productmaterial!;
        }
    }
}