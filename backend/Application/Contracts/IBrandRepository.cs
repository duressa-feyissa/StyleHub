using Domain.Entities;

namespace Application.Contracts
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<IReadOnlyList<Brand>> GetAll();
        Task<Brand> GetById(string id);

        Task<Brand> GetByName(string name);

    }
}