using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.User
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IReadOnlyList<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> GetByPhoneNumber(string phoneNumber);
        Task<User> GetByEmail(string email);
    }
}
