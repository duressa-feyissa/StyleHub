using Domain.Entities;

namespace Application.Contracts
{
    public interface ISizeRepository : IGenericRepository<Size>
    {
        Task<IReadOnlyList<Size>> GetAll();
        Task<Size> GetById(string id);

        Task<Size> GetByName(string name);

        Task<Size> GetByAbbreviation(string abbreviation);

        Task<IReadOnlyList<Size>> GetByIds(List<string> ids);

    }
}