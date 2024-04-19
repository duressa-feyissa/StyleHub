using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<IReadOnlyList<Brand>> GetAll();
        Task<Brand> GetById(string id);

        Task<Brand> GetByName(string name);
    }
}
