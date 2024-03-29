using Application.Contracts.Infrastructure.Repositories;
using Infrastructure.Repository;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddSingleton(CloudinaryConfiguration.Configure(configuration));
            services.AddScoped<IImageUploadRepository, ImageUploadRepository>();

            return services;
        }
    }
}
