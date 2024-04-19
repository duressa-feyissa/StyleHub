using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Persistence.Configuration
{
    public class StyleHubDBContextFactory : IDesignTimeDbContextFactory<StyleHubDBContext>
    {
        public StyleHubDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StyleHubDBContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySQL(connectionString!);

            optionsBuilder.EnableSensitiveDataLogging();
            return new StyleHubDBContext(optionsBuilder.Options);
        }
    }
}
