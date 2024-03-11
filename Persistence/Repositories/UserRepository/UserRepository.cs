using Microsoft.EntityFrameworkCore;
using StyleHub.Application.Contracts;
using StyleHub.Persistence;
using StyleHub.Persistence.Repositories;
using SytleHub.Domain.Entities;

namespace SocialSync.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        StyleHubDBContext _context;
        public UserRepository(StyleHubDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string UserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            return user!;
        }
    }
}