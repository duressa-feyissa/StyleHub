namespace Application.Contracts
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
		Task<int> Save();
	}
}