using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
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