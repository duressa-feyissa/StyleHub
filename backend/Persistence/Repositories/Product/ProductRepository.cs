using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class ProductRepository(StyleHubDBContext context)
        : GenericRepository<Domain.Entities.Product.Product>(context),
            IProductRepository
    {
        public async Task<Domain.Entities.Product.Product> GetById(string id)
        {
            var product = await context
                .Products.Include(p => p.Brand)
                .Include(p => p.User)
                .Include(p => p.ProductImages)
                .ThenInclude(pi => pi.Image)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id && u.IsPublished);

            return product!;
        }

        public async Task<IReadOnlyList<Domain.Entities.Product.Product>> GetAll(
            string search = "",
            string? brandId = null,
            string? userId = null,
            IEnumerable<string>? colorIds = null,
            IEnumerable<string>? materialIds = null,
            IEnumerable<string>? sizeIds = null,
            IEnumerable<string>? categoryIds = null,
            bool? isNegotiable = null,
            float? minPrice = null,
            float? maxPrice = null,
            int? minQuantity = null,
            int? maxQuantity = null,
            string? target = null,
            string? condition = null,
            double? latitude = null,
            double? longitude = null,
            double? radiusInKilometers = null,
            string? sortBy = null,
            string? sortOrder = null,
            int skip = 0,
            int limit = 10
        )
        {
            IQueryable<Domain.Entities.Product.Product> query = context
                .Products
                .Include(p => p.Brand)
                .Include(p => p.User)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductImages)
                .ThenInclude(pi => pi.Image)
                .AsSplitQuery()
                .AsNoTracking();
            query = query.Where(p => p.IsPublished);
            if (latitude != null && longitude != null && radiusInKilometers != null)
            {
                Console.WriteLine("Filtering by location");

                double earthRadius = 6371;

                double minLat =
                    latitude.Value - (radiusInKilometers.Value / earthRadius) * (180 / Math.PI);
                double maxLat =
                    latitude.Value + (radiusInKilometers.Value / earthRadius) * (180 / Math.PI);

                double deltaLon = Math.Asin(
                    Math.Sin(radiusInKilometers.Value / earthRadius)
                        / Math.Cos(latitude.Value * (Math.PI / 180))
                );
                double minLon = longitude.Value - (deltaLon * (180 / Math.PI));
                double maxLon = longitude.Value + (deltaLon * (180 / Math.PI));

                Console.WriteLine(
                    $"minLat: {minLat}, maxLat: {maxLat}, minLon: {minLon}, maxLon: {maxLon}"
                );

                query = query.Where(p =>
                    p.Latitude >= minLat
                    && p.Latitude <= maxLat
                    && p.Longitude >= minLon
                    && p.Longitude <= maxLon
                );
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.Title, $"%{search}%")
                    || EF.Functions.Like(p.Description, $"%{search}%")
                    || EF.Functions.Like(p.City, $"%{search}%")
                );
            }

            if (!string.IsNullOrWhiteSpace(brandId))
            {
                query = query.Where(p => p.Brand.Id == brandId);
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {
                query = query.Where(p => p.User.Id == userId);
            }

            if (colorIds?.Any() == true)
            {
                query = query.Where(p => p.ProductColors.Any(pc => colorIds.Contains(pc.Color.Id)));
            }

            if (materialIds?.Any() == true)
            {
                query = query.Where(p =>
                    p.ProductMaterials.Any(pm => materialIds.Contains(pm.Material.Id))
                );
            }

            if (sizeIds?.Any() == true)
            {
                query = query.Where(p => p.ProductSizes.Any(ps => sizeIds.Contains(ps.Size.Id)));
            }

            if (categoryIds?.Any() == true)
            {
                query = query.Where(p =>
                    p.ProductCategories.Any(pc => categoryIds.Contains(pc.Category.Id))
                );
            }

            if (isNegotiable != null)
            {
                query = query.Where(p => p.IsNegotiable == isNegotiable);
            }

            if (minPrice != null)
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            if (minQuantity != null)
            {
                query = query.Where(p => p.Quantity >= minQuantity);
            }

            if (maxQuantity != null)
            {
                query = query.Where(p => p.Quantity <= maxQuantity);
            }

            if (!string.IsNullOrWhiteSpace(target))
            {
                query = query.Where(p => p.Target == target);
            }

            if (!string.IsNullOrWhiteSpace(condition))
            {
                query = query.Where(p => p.Condition == condition);
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "title":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.Title);
                        else
                            query = query.OrderBy(p => p.Title);
                        break;
                    case "date":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.CreatedAt);
                        else
                            query = query.OrderBy(p => p.CreatedAt);
                        break;
                    case "price":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.Price);
                        else
                            query = query.OrderBy(p => p.Price);
                        break;
                    case "quantity":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.Quantity);
                        else
                            query = query.OrderBy(p => p.Quantity);
                        break;
                    case "condition":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.Condition);
                        else
                            query = query.OrderBy(p => p.Condition);
                        break;
                    case "target":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.Target);
                        else
                            query = query.OrderBy(p => p.Target);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.CreatedAt);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.CreatedAt);
            }

            query = query.Skip(skip).Take(limit);

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.Product.Product>> GetByUserId(
            string userId,
            int skip = 0,
            int limit = 10
        )
        {
            Console.WriteLine($"Fetching products by user {userId}");
            Console.WriteLine($"Skip: {skip}, Limit: {limit}");
            var products = await context
                .Products.Include(p => p.Brand)
          
                .Include(p => p.User)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductImages)
                .ThenInclude(pi => pi.Image)
                .AsSplitQuery()
                .AsNoTracking()
                .Where(p => p.User.Id == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            return products;
        }
    }
}
