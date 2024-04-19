using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class MaterialRepository(StyleHubDBContext context)
        : GenericRepository<Material>(context), IMaterialRepository
    {
        public async Task<IReadOnlyList<Material>> GetAll()
        {
            return await context.Materials.ToListAsync();
        }

        public async Task<Material> GetById(string id)
        {
            var material = await context.Materials.FirstOrDefaultAsync(u => u.Id == id);
            return material!;
        }

        public async Task<Material> GetByName(string name)
        {
            var material = await context.Materials.FirstOrDefaultAsync(u => u.Name == name);
            return material!;
        }

        public async Task<IReadOnlyList<Material>> GetByIds(List<string> ids)
        {
            return await context.Materials.Where(c => ids.Contains(c.Id)).ToListAsync();

        }
    }
}