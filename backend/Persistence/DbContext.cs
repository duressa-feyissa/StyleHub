using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence
{
    public class StyleHubDBContext : DbContext
    {
        public StyleHubDBContext(DbContextOptions<StyleHubDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StyleHubDBContext).Assembly);
        }
    }

}