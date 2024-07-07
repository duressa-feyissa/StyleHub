using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product
{
	public interface IProductDesignRepository : IGenericRepository<ProductDesign>
	{
		Task<ProductDesign> GetById(string id);
		
		Task<bool> DeleteByProductId(string productId);
	}
}