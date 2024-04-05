using Application.Contracts.Persistence.Repositories.Common;
using Application.Contracts.Persistence.Repositories.Product;
using Application.Contracts.Persistence.Repositories.User;

namespace Application.Contracts.Persistance.Repositories
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