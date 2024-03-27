using Domain.Entities;

namespace Application.Contracts
{
    public interface IProductMaterialRepository : IGenericRepository<ProductMaterial>
    {
        Task<ProductMaterial> GetById(string id);
    }
}