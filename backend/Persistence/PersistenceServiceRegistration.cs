using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence
{
	public static class PersistenceServiceRegistration
	{

		public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<StyleHubDBContext>(options =>
			{
				options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
			});


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
			services.AddScoped<IProductImageRepository, ProductImageRepository>();
			services.AddScoped<ILocationRepository, LocationRepository>();

			return services;
		}

	}
}