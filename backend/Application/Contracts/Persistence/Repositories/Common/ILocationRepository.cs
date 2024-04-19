using backend.Domain.Entities.Common;

namespace backend.Application.Contracts.Persistence.Repositories.Common
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<IReadOnlyList<Location>> GetAll();
        Task<Location> GetById(string id);

        Task<Location> GetByName(string name);

        Task<Location> GetByCoordinates(double latitude, double longitude);
    }
}
