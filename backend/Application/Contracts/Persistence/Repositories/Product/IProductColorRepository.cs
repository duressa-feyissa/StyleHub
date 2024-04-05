using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Product;

namespace Application.Contracts.Persistence.Repositories.Product
{
	public interface IProductColorRepository : IGenericRepository<ProductColor>
	{
		Task<ProductColor> GetById(string id);
		
		Task<bool> DeleteByProductId(string productId);
	}
}