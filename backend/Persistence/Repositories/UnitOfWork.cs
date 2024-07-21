using backend.Application.Contracts.Persistence;
using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Application.Contracts.Persistence.Repositories.Shop;
using backend.Application.Contracts.Persistence.Repositories.User;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using backend.Persistence.Repositories.Product;
using backend.Persistence.Repositories.Shop;
using backend.Persistence.Repositories.User;

namespace backend.Persistence.Repositories
{
    public class UnitOfWork(
        StyleHubDBContext context,
        IProductRepository productRepository,
        IColorRepository colorRepository,
        ISizeRepository sizeRepository,
        IBrandRepository brandRepository,
        IMaterialRepository materialRepository,
        IProductColorRepository productColorRepository,
        IProductSizeRepository productSizeRepository,
        IProductMaterialRepository productMaterialRepository,
        ILocationRepository locationRepository,
        ICategoryRepository categoryRepository,
        IProductCategoryRepository productCategoryRepository,
        IRoleRepository roleRepository,
        IUserRepository userRepository,
        IDesignRepository designRepository,
        IProductBrandRepository productBrandRepository,
        IProductDesignRepository productDesignRepository,
        IProductViewRepository productViewRepository,
        IFavouriteProductRepository favouriteProductRepository,
        IContactedProductRepository contactedProductRepository,
        IShopRepository shopRepository,
        IWorkingHourRepository workingHourRepository,
        IImageRepository imageRepository,
        IShopReviewRepository shopReviewRepository
        )
        : IUnitOfWork
    {
        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(context);
                return productRepository;
            }
        }

        public IColorRepository ColorRepository
        {
            get
            {
                if (colorRepository == null)
                    colorRepository = new ColorRepository(context);
                return colorRepository;
            }
        }

        public ISizeRepository SizeRepository
        {
            get
            {
                if (sizeRepository == null)
                    sizeRepository = new SizeRepository(context);
                return sizeRepository;
            }
        }

        public IBrandRepository BrandRepository
        {
            get
            {
                if (brandRepository == null)
                    brandRepository = new BrandRepository(context);
                return brandRepository;
            }
        }

        public IMaterialRepository MaterialRepository
        {
            get
            {
                if (materialRepository == null)
                    materialRepository = new MaterialRepository(context);
                return materialRepository;
            }
        }

        public IProductColorRepository ProductColorRepository
        {
            get
            {
                if (productColorRepository == null)
                    productColorRepository = new ProductColorRepository(context);
                return productColorRepository;
            }
        }

        public IProductSizeRepository ProductSizeRepository
        {
            get
            {
                if (productSizeRepository == null)
                    productSizeRepository = new ProductSizeRepository(context);
                return productSizeRepository;
            }
        }

        public IProductMaterialRepository ProductMaterialRepository
        {
            get
            {
                if (productMaterialRepository == null)
                    productMaterialRepository = new ProductMaterialRepository(context);
                return productMaterialRepository;
            }
        }

        public ILocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null)
                    locationRepository = new LocationRepository(context);
                return locationRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(context);
                return categoryRepository;
            }
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get
            {
                if (productCategoryRepository == null)
                    productCategoryRepository = new ProductCategoryRepository(context);
                return productCategoryRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(context);
                return roleRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(context);
                return userRepository;
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if (imageRepository == null)
                    imageRepository = new ImageRepository(context);
                return imageRepository;
            }
        }
        
        public IDesignRepository DesignRepository
        {
            get
            {
                if (designRepository == null)
                    designRepository = new DesignRepository(context);
                return designRepository;
            }
        }
        
        public IProductBrandRepository ProductBrandRepository
        {
            get
            {
                if (productBrandRepository == null)
                    productBrandRepository = new ProductBrandRepository(context);
                return productBrandRepository;
            }
        }
        
        public IProductDesignRepository ProductDesignRepository
        {
            get
            {
                if (productDesignRepository == null)
                    productDesignRepository = new ProductDesignRepository(context);
                return productDesignRepository;
            }
        }
        
        public IProductViewRepository ProductViewRepository
        {
            get
            {
                if (productViewRepository == null)
                    productViewRepository = new ProductViewRepository(context);
                return productViewRepository;
            }
        }
        
        public IFavouriteProductRepository FavouriteProductRepository
        {
            get
            {
                if (favouriteProductRepository == null)
                    favouriteProductRepository = new FavouriteProductRepository(context);
                return favouriteProductRepository;
            }
        }
        
        public IContactedProductRepository ContactedProductRepository
        {
            get
            {
                if (contactedProductRepository == null)
                    contactedProductRepository = new ContactedProductRepository(context);
                return contactedProductRepository;
            }
        }
        
        public IShopRepository ShopRepository
        {
            get
            {
                if (shopRepository == null)
                    shopRepository = new ShopRepository(context);
                return shopRepository;
            }
        }
        
        public IWorkingHourRepository WorkingHourRepository
        {
            get
            {
                if (workingHourRepository == null)
                    workingHourRepository = new WorkingHourRepository(context);
                return workingHourRepository;
            }
        }
        
        public IShopReviewRepository ShopReviewRepository
        {
            get
            {
                if (shopReviewRepository == null)
                    shopReviewRepository = new ShopReviewRepository(context);
                return shopReviewRepository;
            }
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }
    }
}
