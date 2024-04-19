using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class SizeRepository(StyleHubDBContext context) : GenericRepository<Size>(context), ISizeRepository
    {
        public async Task<IReadOnlyList<Size>> GetAll()
        {
            return await context.Sizes.ToListAsync();
        }

        public async Task<Size> GetById(string id)
        {
            var size = await context.Sizes.FirstOrDefaultAsync(u => u.Id == id);
            return size!;
        }

        public async Task<Size> GetByName(string name)
        {
            var size = await context.Sizes.FirstOrDefaultAsync(u => u.Name == name);
            return size!;
        }

        public async Task<Size> GetByAbbreviation(string abbreviation)
        {
            var size = await context.Sizes.FirstOrDefaultAsync(u =>
                u.Abbreviation == abbreviation
            );
            return size!;
        }

        public async Task<IReadOnlyList<Size>> GetByIds(List<string> ids)
        {
            return await context.Sizes.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
