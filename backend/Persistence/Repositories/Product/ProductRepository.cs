using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
{
    public class ProductRepository
        : GenericRepository<Domain.Entities.Product.Product>,
            IProductRepository
    {
        private readonly StyleHubDBContext _context;

        public ProductRepository(StyleHubDBContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Product.Product> GetById(string id)
        {
            var product = await _context
                .Products.Include(p => p.Brand)
                .Include(p => p.Images)
                .Include(p => p.Location)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
            return product!;
        }

        public async Task<IReadOnlyList<Domain.Entities.Product.Product>> GetAll(
            string search = "",
            string? brandId = null,
            IEnumerable<string>? colorIds = null,
            IEnumerable<string>? materialIds = null,
            IEnumerable<string>? sizeIds = null,
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
            int skip = 0,
            int limit = 10
        )
        {
            IQueryable<Domain.Entities.Product.Product> query = _context
                .Products.Include(p => p.Images)
                .Include(p => p.Location)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .AsSplitQuery()
                .AsNoTracking();

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
                    p.Location.Latitude >= minLat
                    && p.Location.Latitude <= maxLat
                    && p.Location.Longitude >= minLon
                    && p.Location.Longitude <= maxLon
                );
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.Title, $"%{search}%")
                    || EF.Functions.Like(p.Description, $"%{search}%")
                );
            }

            if (!string.IsNullOrWhiteSpace(brandId))
            {
                query = query.Where(p => p.Brand.Id == brandId);
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

            query = query.OrderBy(p => p.Id).Skip(skip).Take(limit);
            return await query.ToListAsync();
        }
    }
}
