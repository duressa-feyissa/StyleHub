using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
{
    public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
    {
        StyleHubDBContext _context;
        public MaterialRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Material>> GetAll()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task<Material> GetById(string id)
        {
            var material = await _context.Materials.FirstOrDefaultAsync(u => u.Id == id);
            return material!;
        }

        public async Task<Material> GetByName(string name)
        {
            var material = await _context.Materials.FirstOrDefaultAsync(u => u.Name == name);
            return material!;
        }

        public async Task<IReadOnlyList<Material>> GetByIds(List<string> ids)
        {
            return await _context.Materials.Where(c => ids.Contains(c.Id)).ToListAsync();

        }
    }
}