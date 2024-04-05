using Application.Contracts.Persistance.Repositories;
using Application.Contracts.Persistence.Repositories.Common;
using Application.Contracts.Persistence.Repositories.Product;
using Application.Contracts.Persistence.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Persistence.Repositories.Common;
using Persistence.Repositories.Product;
using Persistence.Repositories.User;

namespace Persistence.Configuration
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment hostEnvironment
        )
        {
            if (hostEnvironment.IsDevelopment())
            {
                services.AddDbContext<StyleHubDBContext>(options =>
                {
                    options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
                });
            }
            else
            {
                var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
                services.AddDbContext<StyleHubDBContext>(options =>
                {
                    options.UseMySQL(connectionString!);
                });
            }

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
            services.AddScoped<IProductMaterialRepository, ProductMaterialRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            return services;
        }
    }
}
