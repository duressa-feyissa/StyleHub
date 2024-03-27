using Domain.Entities;

namespace Application.Contracts
{
    public interface IMaterialRepository : IGenericRepository<Material>
    {
        Task<IReadOnlyList<Material>> GetAll();
        Task<Material> GetById(string id);

        Task<Material> GetByName(string name);

        Task<IReadOnlyList<Material>> GetByIds(List<string> ids);

    }
}