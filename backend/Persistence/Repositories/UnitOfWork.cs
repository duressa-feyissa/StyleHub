using Application.Contracts.Persistance.Repositories;
using Application.Contracts.Persistence.Repositories.Common;
using Application.Contracts.Persistence.Repositories.Product;
using Persistence.Configuration;
using Persistence.Repositories.Common;
using Persistence.Repositories.Product;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StyleHubDBContext _context;
        private IProductRepository _productRepository;

        private IColorRepository _colorRepository;

        private ISizeRepository _sizeRepository;

        private IBrandRepository _brandRepository;

        private IMaterialRepository _materialRepository;

        private IProductColorRepository _productColorRepository;

        private IProductSizeRepository _productSizeRepository;

        private IProductMaterialRepository _productMaterialRepository;

        private IProductImageRepository _productImageRepository;

        private ILocationRepository _locationRepository;

        private ICategoryRepository _categoryRepository;

        private IProductCategoryRepository _productCategoryRepository;

        public UnitOfWork(
            StyleHubDBContext context,
            IProductRepository productRepository,
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IBrandRepository brandRepository,
            IMaterialRepository materialRepository,
            IProductColorRepository productColorRepository,
            IProductSizeRepository productSizeRepository,
            IProductMaterialRepository productMaterialRepository,
            IProductImageRepository productImageRepository,
            ILocationRepository locationRepository,
            ICategoryRepository categoryRepository,
            IProductCategoryRepository productCategoryRepository
        )
        {
            _context = context;
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _brandRepository = brandRepository;
            _materialRepository = materialRepository;
            _productColorRepository = productColorRepository;
            _productSizeRepository = productSizeRepository;
            _productMaterialRepository = productMaterialRepository;
            _productImageRepository = productImageRepository;
            _locationRepository = locationRepository;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }

        public IColorRepository ColorRepository
        {
            get
            {
                if (_colorRepository == null)
                    _colorRepository = new ColorRepository(_context);
                return _colorRepository;
            }
        }

        public ISizeRepository SizeRepository
        {
            get
            {
                if (_sizeRepository == null)
                    _sizeRepository = new SizeRepository(_context);
                return _sizeRepository;
            }
        }

        public IBrandRepository BrandRepository
        {
            get
            {
                if (_brandRepository == null)
                    _brandRepository = new BrandRepository(_context);
                return _brandRepository;
            }
        }

        public IMaterialRepository MaterialRepository
        {
            get
            {
                if (_materialRepository == null)
                    _materialRepository = new MaterialRepository(_context);
                return _materialRepository;
            }
        }

        public IProductColorRepository ProductColorRepository
        {
            get
            {
                if (_productColorRepository == null)
                    _productColorRepository = new ProductColorRepository(_context);
                return _productColorRepository;
            }
        }

        public IProductSizeRepository ProductSizeRepository
        {
            get
            {
                if (_productSizeRepository == null)
                    _productSizeRepository = new ProductSizeRepository(_context);
                return _productSizeRepository;
            }
        }

        public IProductMaterialRepository ProductMaterialRepository
        {
            get
            {
                if (_productMaterialRepository == null)
                    _productMaterialRepository = new ProductMaterialRepository(_context);
                return _productMaterialRepository;
            }
        }

        public IProductImageRepository ProductImageRepository
        {
            get
            {
                if (_productImageRepository == null)
                    _productImageRepository = new ProductImageRepository(_context);
                return _productImageRepository;
            }
        }

        public ILocationRepository LocationRepository
        {
            get
            {
                if (_locationRepository == null)
                    _locationRepository = new LocationRepository(_context);
                return _locationRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get
            {
                if (_productCategoryRepository == null)
                    _productCategoryRepository = new ProductCategoryRepository(_context);
                return _productCategoryRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
