using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
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