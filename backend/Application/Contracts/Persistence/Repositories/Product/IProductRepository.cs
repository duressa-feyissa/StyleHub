using backend.Application.Contracts.Persistence.Repositories.Common;

namespace backend.Application.Contracts.Persistence.Repositories.Product
{
	public interface IProductRepository : IGenericRepository<Domain.Entities.Product.Product>
	{
		Task<IReadOnlyList<Domain.Entities.Product.Product>> GetAll(
			string search = "",
			IEnumerable<string>? colorIds = null,
			IEnumerable<string>? materialIds = null,
			IEnumerable<string>? sizeIds = null,
			IEnumerable<string>? categoryIds = null,
			IEnumerable<string>? brandIds = null,
			IEnumerable<string>? designIds = null,
			string? userId = null,
			string? shopId = null,
			bool? isNegotiable = null,
			float? minPrice = null,
			float? maxPrice = null,
			string? status = null,
			bool? inStock = null,
			string? condition = null,
			double? latitude = null,
			double? longitude = null,
			double? radiusInKilometers = null,
			string? sortBy = null,
			string? sortOrder = null,
			int skip = 0,
			int limit = 10
		);

		Task<Domain.Entities.Product.Product> GetById(string id);
		
	}
}
