using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
{
    public class ProductMaterialRepository : GenericRepository<ProductMaterial>, IProductMaterialRepository
    {
        StyleHubDBContext _context;
        public ProductMaterialRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteByProductId(string productId)
        {
            var productmaterials = await _context.ProductMaterials.Where(u => u.ProductId == productId).ToListAsync();
            _context.ProductMaterials.RemoveRange(productmaterials);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductMaterial> GetById(string id)
        {
            var productmaterial = await _context.ProductMaterials.FirstOrDefaultAsync(u => u.Id == id);
            return productmaterial!;
        }
    }
}