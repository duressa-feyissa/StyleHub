using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductMaterialRepository : GenericRepository<ProductMaterial>, IProductMaterialRepository
    {
        StyleHubDBContext _context;
        public ProductMaterialRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductMaterial> GetById(string id)
        {
            var productmaterial = await _context.ProductMaterials.FirstOrDefaultAsync(u => u.Id == id);
            return productmaterial!;
        }
    }
}