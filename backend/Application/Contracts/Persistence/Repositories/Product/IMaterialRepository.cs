using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
    public interface IMaterialRepository : IGenericRepository<Material>
    {
        Task<IReadOnlyList<Material>> GetAll();
        Task<Material> GetById(string id);

        Task<Material> GetByName(string name);

        Task<IReadOnlyList<Material>> GetByIds(List<string> ids);

    }
}