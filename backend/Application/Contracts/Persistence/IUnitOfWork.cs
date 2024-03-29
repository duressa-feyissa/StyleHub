using Application.Contracts.Persistence.Repositories.Common;
using Application.Contracts.Persistence.Repositories.Product;

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
        IProductImageRepository ProductImageRepository { get; }
        ILocationRepository LocationRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        Task<int> Save();
    }
}
