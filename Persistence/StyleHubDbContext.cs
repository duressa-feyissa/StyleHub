using Microsoft.EntityFrameworkCore;
using SytleHub.Domain.Entities;

namespace StyleHub.Persistence
{
    public class StyleHubDBContext : DbContext
    {
        public StyleHubDBContext(DbContextOptions<StyleHubDBContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StyleHubDBContext).Assembly);
        }
    }

}