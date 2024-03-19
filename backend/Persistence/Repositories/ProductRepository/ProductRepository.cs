using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        StyleHubDBContext _context;
        public ProductRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            var user = await _context.Products.FirstOrDefaultAsync(u => u.Id == id);
            return user!;
        }

    }
}