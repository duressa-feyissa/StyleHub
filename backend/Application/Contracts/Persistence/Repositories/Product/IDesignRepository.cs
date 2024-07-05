using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product
{
    public interface IDesignRepository : IGenericRepository<Design>
    {
        Task<IReadOnlyList<Design>> GetAll();
        Task<Design> GetById(string id);
        Task<Design> GetByName(string name);
        Task<IReadOnlyList<Design>> GetByIds(List<string> ids);
    }
}