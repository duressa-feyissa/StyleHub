using Microsoft.EntityFrameworkCore;
using StyleHub.Application.Contracts;
using StyleHub.Persistence.Repositories;
using SytleHub.Application.Contracts;

namespace StyleHub.Persistence
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

            return services;
        }

    }
}