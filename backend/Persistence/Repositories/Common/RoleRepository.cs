using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence.Repositories.Common
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        StyleHubDBContext _context;

        public RoleRepository(StyleHubDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetByCode(string code)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(u => u.Code == code);
            return role!;
        }

        public async Task<Role> GetById(string id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(u => u.Id == id);
            return role!;
        }

        public async Task<Role> GetByName(string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(u => u.Name == name);
            return role!;
        }
    }
}
