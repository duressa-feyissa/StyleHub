using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Common;
using backend.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Common
{
    public class RoleRepository(StyleHubDBContext context) : GenericRepository<Role>(context), IRoleRepository
    {
        public async Task<IReadOnlyList<Role>> GetAll()
        {
            return await context.Roles.ToListAsync();
        }

        public async Task<Role> GetByCode(string code)
        {
            var role = await context.Roles.FirstOrDefaultAsync(u => u.Code == code);
            return role!;
        }

        public async Task<Role> GetById(string id)
        {
            var role = await context.Roles.FirstOrDefaultAsync(u => u.Id == id);
            return role!;
        }

        public async Task<Role> GetByName(string name)
        {
            var role = await context.Roles.FirstOrDefaultAsync(u => u.Name == name);
            return role!;
        }
    }
}
