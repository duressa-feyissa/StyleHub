using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
    {
        Task<ProductCategory> GetById(string id);

        Task<bool> DeleteByProductId(string productId);
    }
}
