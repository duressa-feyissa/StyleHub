using Domain.Entities;

namespace Application.Contracts
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