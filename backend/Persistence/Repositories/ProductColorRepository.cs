using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductColorRepository : GenericRepository<ProductColor>, IProductColorRepository
    {
        StyleHubDBContext _context;
        public ProductColorRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductColor> GetById(string id)
        {
            var productcolor = await _context.ProductColors.FirstOrDefaultAsync(u => u.Id == id);
            return productcolor!;
        }
    }
}