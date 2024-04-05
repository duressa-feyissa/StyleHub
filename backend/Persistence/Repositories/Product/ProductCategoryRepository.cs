using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
{
    public class ProductCategoryRepository
        : GenericRepository<ProductCategory>,
            IProductCategoryRepository
    {
        StyleHubDBContext _context;

        public ProductCategoryRepository(StyleHubDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteByProductId(string productId)
        {
            var productCategories = await _context
                .ProductCategories.Where(u => u.ProductId == productId)
                .ToListAsync();
            _context.ProductCategories.RemoveRange(productCategories);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductCategory> GetById(string id)
        {
            var productcolor = await _context.ProductCategories.FirstOrDefaultAsync(u =>
                u.Id == id
            );
            return productcolor!;
        }
    }
}
