using Application.Contracts.Persistence.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.User
{
    public class UserRepository : GenericRepository<Domain.Entities.User.User>, IUserRepository
    {
        private readonly StyleHubDBContext _context;

        public UserRepository(StyleHubDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Domain.Entities.User.User>> GetAll()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<Domain.Entities.User.User> GetById(string id)
        {
            var user = await _context
                .Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user!;
        }

        public async Task<Domain.Entities.User.User> GetByPhoneNumber(string phoneNumber)
        {
            var user = await _context
                .Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return user!;
        }

        public async Task<Domain.Entities.User.User> GetByEmail(string email)
        {
            var user = await _context
                .Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
            return user!;
        }

        public async Task<bool> IsPhoneNumberRegistered(string phoneNumber)
        {
            return await _context
                .Users.Include(u => u.Role)
                .AnyAsync(u => u.PhoneNumber == phoneNumber);
        }
    }
}
