using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<IReadOnlyList<Brand>> GetAll();
        Task<Brand> GetById(string id);

        Task<Brand> GetByName(string name);
    }
}
