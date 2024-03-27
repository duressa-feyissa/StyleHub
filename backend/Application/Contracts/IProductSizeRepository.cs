using Domain.Entities;

namespace Application.Contracts
{
    public interface IProductSizeRepository : IGenericRepository<ProductSize>
    {
        Task<ProductSize> GetById(string id);
    }
}