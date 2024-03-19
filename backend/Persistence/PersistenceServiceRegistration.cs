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

            return services;
        }

    }
}