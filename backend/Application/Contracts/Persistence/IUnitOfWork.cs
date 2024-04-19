using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Application.Contracts.Persistence.Repositories.User;

namespace backend.Application.Contracts.Persistence
{
	public interface IUnitOfWork
	{
		IColorRepository ColorRepository { get; }
		IProductRepository ProductRepository { get; }
		ISizeRepository SizeRepository { get; }
		IBrandRepository BrandRepository { get; }
		IMaterialRepository MaterialRepository { get; }
		IProductColorRepository ProductColorRepository { get; }
		IProductSizeRepository ProductSizeRepository { get; }
		IProductMaterialRepository ProductMaterialRepository { get; }
		ILocationRepository LocationRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		IRoleRepository RoleRepository { get; }
		IProductCategoryRepository ProductCategoryRepository { get; }
		IUserRepository UserRepository { get; }
		IImageRepository ImageRepository { get; }
		Task<int> Save();
	}
}
