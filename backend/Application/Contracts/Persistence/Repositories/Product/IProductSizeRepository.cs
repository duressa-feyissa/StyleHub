using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
    public interface IProductSizeRepository : IGenericRepository<ProductSize>
    {
        Task<ProductSize> GetById(string id);
    }
}
