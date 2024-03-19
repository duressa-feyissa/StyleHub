using Application.Contracts;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetAll();
        Task<Product> GetById(string id);

    }
}