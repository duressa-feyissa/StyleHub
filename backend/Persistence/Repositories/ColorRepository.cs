using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        StyleHubDBContext _context;
        public ColorRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Color>> GetAll()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<Color> GetById(string id)
        {
            var user = await _context.Colors.FirstOrDefaultAsync(u => u.Id == id);
            return user!;
        }

        public async Task<Color> GetByName(string name)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(c => c.Name == name);
            return color!;
        }

        public async Task<Color> GetByHexCode(string hexCode)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(c => c.HexCode == hexCode);
            return color!;
        }

        public async Task<IReadOnlyList<Color>> GetByIds(List<string> ids)
        {
            return await _context.Colors.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

    }
}