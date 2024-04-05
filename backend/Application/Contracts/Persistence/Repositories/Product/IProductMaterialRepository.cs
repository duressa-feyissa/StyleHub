using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
	public interface IProductMaterialRepository : IGenericRepository<ProductMaterial>
	{
		Task<ProductMaterial> GetById(string id);
		
		Task<bool> DeleteByProductId (string productId);
	}
}