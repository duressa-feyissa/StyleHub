using Domain.Entities;

namespace Application.Contracts
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        Task<ProductImage> GetById(string id);
    }
}