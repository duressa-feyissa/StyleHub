using Domain.Entities;

namespace Application.Contracts
{
    public interface IProductColorRepository : IGenericRepository<ProductColor>
    {
        Task<ProductColor> GetById(string id);
    }
}