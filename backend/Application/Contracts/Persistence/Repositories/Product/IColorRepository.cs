using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        Task<IReadOnlyList<Color>> GetAll();
        Task<Color> GetById(string id);

        Task<Color> GetByName(string name);

        Task<Color> GetByHexCode(string hexCode);

        Task<IReadOnlyList<Color>> GetByIds(List<string> ids);

    }
}