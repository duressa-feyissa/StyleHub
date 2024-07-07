using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product
{
	public interface IProductBrandRepository : IGenericRepository<ProductBrand>
	{
		Task<ProductBrand> GetById(string id);
		
		Task<bool> DeleteByProductId(string productId);
	}
}