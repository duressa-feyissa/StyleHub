using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ColorRepository(StyleHubDBContext context) : GenericRepository<Color>(context), IColorRepository
    {
        public async Task<IReadOnlyList<Color>> GetAll()
        {
            return await context.Colors.ToListAsync();
        }

        public async Task<Color> GetById(string id)
        {
            var user = await context.Colors.FirstOrDefaultAsync(u => u.Id == id);
            return user!;
        }

        public async Task<Color> GetByName(string name)
        {
            var color = await context.Colors.FirstOrDefaultAsync(c => c.Name == name);
            return color!;
        }

        public async Task<Color> GetByHexCode(string hexCode)
        {
            var color = await context.Colors.FirstOrDefaultAsync(c => c.HexCode == hexCode);
            return color!;
        }

        public async Task<IReadOnlyList<Color>> GetByIds(List<string> ids)
        {
            return await context.Colors.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

    }
}