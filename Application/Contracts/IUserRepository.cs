using SytleHub.Application.Contracts;
using SytleHub.Domain.Entities;

namespace StyleHub.Application.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<List<User>> GetAllUser();
        public Task<User> GetUserById(string UserId);

    }
}