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
                .Products
                .Include(p => p.ProductImages)
                .ThenInclude(pi => pi.Image)
                .Include( p => p.Shop)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductDesigns)
                .ThenInclude(pd => pd.Design)
                .Include(p => p.ProductBrands)
                .ThenInclude(pb => pb.Brand)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return product!;
        }

        public async Task<IReadOnlyList<Domain.Entities.Product.Product>> GetAll(
            string search = "",
            IEnumerable<string>? colorIds = null,
            IEnumerable<string>? materialIds = null,
            IEnumerable<string>? sizeIds = null,
            IEnumerable<string>? categoryIds = null,
            IEnumerable<string>? brandIds = null,
            IEnumerable<string>? designIds = null,
            string? userId = null,
            bool? isNegotiable = null,
            float? minPrice = null,
            float? maxPrice = null,
            string? shopId = null,
            string? status = null,
            bool? inStock = null,
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
                .Include(p => p.Shop)
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
                .Include(p => p.ProductDesigns)
                .ThenInclude(pd => pd.Design)
                .Include(p => p.ProductBrands)
                .ThenInclude(pb => pb.Brand)
                .AsSplitQuery()
                .AsNoTracking();
            query = query.Where(p => p.Status == (status ?? "active"));
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
                    p.Shop.Latitude >= minLat
                    && p.Shop.Latitude <= maxLat
                    && p.Shop.Longitude >= minLon
                    && p.Shop.Longitude <= maxLon
                );
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.Title, $"%{search}%")
                    || EF.Functions.Like(p.Description, $"%{search}%")
                    || EF.Functions.Like(p.Shop.City, $"%{search}%")
                    || EF.Functions.Like(p.Shop.State, $"%{search}%")
                    || EF.Functions.Like(p.Shop.Country, $"%{search}%")
                    || EF.Functions.Like(p.Shop.StreetAddress, $"%{search}%")
                );
            }
            
            if (brandIds?.Any() == true)
            {
                query = query.Where(p =>
                    p.ProductBrands.Any(pb => brandIds.Contains(pb.Brand.Id))
                );
            }
            
            if (designIds?.Any() == true)
            {
                query = query.Where(p =>
                    p.ProductDesigns.Any(pd => designIds.Contains(pd.Design.Id))
                );
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
            
            if (!string.IsNullOrWhiteSpace(condition))
            {
                query = query.Where(p => p.Condition == condition);
            }
            
            if (!string.IsNullOrWhiteSpace(shopId))
            {
                query = query.Where(p => p.ShopId == shopId);
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
                    case "inStock":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.InStock);
                        else
                            query = query.OrderBy(p => p.InStock);
                        break;
                    case "condition":
                        if (sortOrder?.ToLower() == "desc")
                            query = query.OrderByDescending(p => p.Condition);
                        else
                            query = query.OrderBy(p => p.Condition);
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
            
            var products = await query.ToListAsync();

            return products;
        }
    }
}
