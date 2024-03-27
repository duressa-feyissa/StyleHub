using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class StyleHubDBContext : DbContext
    {
        public StyleHubDBContext(DbContextOptions<StyleHubDBContext> options)
            : base(options) { }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Color> Colors { get; set; }

        public virtual DbSet<Size> Sizes { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Material> Materials { get; set; }

        public virtual DbSet<ProductColor> ProductColors { get; set; }

        public virtual DbSet<ProductSize> ProductSizes { get; set; }

        public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }

        public virtual DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasKey(c => c.Id);
            modelBuilder.Entity<Color>().HasKey(s => s.Id);
            modelBuilder.Entity<Size>().HasKey(s => s.Id);
            modelBuilder.Entity<Brand>().HasKey(b => b.Id);
            modelBuilder.Entity<Material>().HasKey(m => m.Id);
            modelBuilder.Entity<ProductColor>().HasKey(pc => pc.Id);
            modelBuilder.Entity<ProductSize>().HasKey(ps => ps.Id);
            modelBuilder.Entity<ProductMaterial>().HasKey(pm => pm.Id);
            modelBuilder.Entity<ProductImage>().HasKey(pi => pi.Id);
            modelBuilder.Entity<Location>().HasKey(l => l.Id);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StyleHubDBContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

     
    }
}
