using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetAll();
        Task<Category> GetById(string id);

        Task<Category> GetByName(string name);

        Task<IReadOnlyList<Category>> GetByIds(List<string> ids);
    }
}
