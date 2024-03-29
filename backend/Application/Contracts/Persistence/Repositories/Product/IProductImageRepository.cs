using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        Task<ProductImage> GetById(string id);
    }
}