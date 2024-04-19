using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product
{
    public interface IMaterialRepository : IGenericRepository<Material>
    {
        Task<IReadOnlyList<Material>> GetAll();
        Task<Material> GetById(string id);

        Task<Material> GetByName(string name);

        Task<IReadOnlyList<Material>> GetByIds(List<string> ids);

    }
}