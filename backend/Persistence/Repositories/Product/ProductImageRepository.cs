using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        StyleHubDBContext _context;
        public ProductImageRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductImage> GetById(string id)
        {
            var productcolor = await _context.ProductImages.FirstOrDefaultAsync(u => u.Id == id);
            return productcolor!;
        }
    }
}