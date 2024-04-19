using backend.Application.Contracts.Persistence.Repositories.User;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.User
{
    public class UserRepository(StyleHubDBContext context)
        : GenericRepository<Domain.Entities.User.User>(context), IUserRepository
    {
        public async Task<Domain.Entities.User.User> GetById(string id)
        {
            var user = await context
                .Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user!;
        }

        public async Task<Domain.Entities.User.User> GetByPhoneNumber(string phoneNumber)
        {
            var user = await context
                .Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return user!;
        }

        public async Task<Domain.Entities.User.User> GetByEmail(string email)
        {
            var user = await context
                .Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
            return user!;
        }

        public async Task<bool> IsPhoneNumberRegistered(string phoneNumber)
        {
            return await context
                .Users.Include(u => u.Role)
                .AnyAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<IReadOnlyList<Domain.Entities.User.User>> GetAll(
            int skip,
            int limit,
            string search,
            string sortBy,
            string orderBy,
            bool isVerified
        )
        {
            var query = context.Users.Include(u => u.Role).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u =>
                    (u.FirstName != null && u.FirstName.Contains(search))
                    || (u.LastName != null && u.LastName.Contains(search))
                    || (u.PhoneNumber != null && u.PhoneNumber.Contains(search))
                    || (u.Email != null && u.Email.Contains(search))
                    || (u.Country != null && u.Country.Contains(search))
                    || (u.City != null && u.City.Contains(search))
                    || (u.Address != null && u.Address.Contains(search))
                );
            }

            if (isVerified)
            {
                query = query.Where(u => u.IsEmailVerified);
            }

            query = sortBy switch
            {
                "firstName"
                    => orderBy == "asc"
                        ? query.OrderBy(u => u.FirstName)
                        : query.OrderByDescending(u => u.FirstName),
                "lastName"
                    => orderBy == "asc"
                        ? query.OrderBy(u => u.LastName)
                        : query.OrderByDescending(u => u.LastName),
                "phoneNumber"
                    => orderBy == "asc"
                        ? query.OrderBy(u => u.PhoneNumber)
                        : query.OrderByDescending(u => u.PhoneNumber),
                "email"
                    => orderBy == "asc"
                        ? query.OrderBy(u => u.Email)
                        : query.OrderByDescending(u => u.Email),
                "country"
                    => orderBy == "asc"
                        ? query.OrderBy(u => u.Country)
                        : query.OrderByDescending(u => u.Country),
                "city"
                    => orderBy == "asc"
                        ? query.OrderBy(u => u.City)
                        : query.OrderByDescending(u => u.City),
                "address"
                    => orderBy == "asc"
                        ? query.OrderBy(u => u.Address)
                        : query.OrderByDescending(u => u.Address),
                _ => query
            };

            query = query.Skip(skip).Take(limit);

            return await query.ToListAsync();
        }
    }
}
