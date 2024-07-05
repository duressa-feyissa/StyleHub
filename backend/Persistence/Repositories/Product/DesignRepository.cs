using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class DesignRepository(StyleHubDBContext context)
        : GenericRepository<Design>(context), IDesignRepository
    {
        public async Task<IReadOnlyList<Design>> GetAll()
        {
            return await context.Designs.OrderBy(u => u.Name).ToListAsync();
        }

        public async Task<Design> GetById(string id)
        {
            var design = await context.Designs.FirstOrDefaultAsync(u => u.Id == id);
            return design!;
        }

        public async Task<Design> GetByName(string name)
        {
            var design = await context.Designs.FirstOrDefaultAsync(u => u.Name == name);
            return design!;
        }

        public async Task<IReadOnlyList<Design>> GetByIds(List<string> ids)
        {
            return await context.Designs.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}