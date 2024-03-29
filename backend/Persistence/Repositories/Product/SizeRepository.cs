using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        StyleHubDBContext _context;

        public SizeRepository(StyleHubDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Size>> GetAll()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Size> GetById(string id)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(u => u.Id == id);
            return size!;
        }

        public async Task<Size> GetByName(string name)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(u => u.Name == name);
            return size!;
        }

        public async Task<Size> GetByAbbreviation(string abbreviation)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(u =>
                u.Abbreviation == abbreviation
            );
            return size!;
        }

        public async Task<IReadOnlyList<Size>> GetByIds(List<string> ids)
        {
            return await _context.Sizes.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
