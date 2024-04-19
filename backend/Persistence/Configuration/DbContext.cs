using backend.Domain.Entities.Common;
using backend.Domain.Entities.Product;
using backend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Configuration
{
	public class StyleHubDBContext(DbContextOptions<StyleHubDBContext> options) : DbContext(options)
	{
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Color> Colors { get; set; }
		public virtual DbSet<Size> Sizes { get; set; }
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Material> Materials { get; set; }
		public virtual DbSet<ProductColor> ProductColors { get; set; }
		public virtual DbSet<ProductSize> ProductSizes { get; set; }
		public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }
		public virtual DbSet<Location> Locations { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<ProductCategory> ProductCategories { get; set; }
		
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Image> Images { get; set; }

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
			modelBuilder.Entity<Location>().HasKey(l => l.Id);
			modelBuilder.Entity<Category>().HasKey(c => c.Id);
			modelBuilder.Entity<ProductCategory>().HasKey(pc => pc.Id);
			modelBuilder.Entity<ProductImage>().HasKey(pi => pi.Id);
			modelBuilder.Entity<User>().HasKey(u => u.Id);
			modelBuilder.Entity<Role>().HasKey(r => r.Id);
			modelBuilder.Entity<Image>().HasKey(i => i.Id);
			base.OnModelCreating(modelBuilder);
		}
	}
}
