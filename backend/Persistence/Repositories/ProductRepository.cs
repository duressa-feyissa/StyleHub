using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly StyleHubDBContext _context;

		public ProductRepository(StyleHubDBContext context)
			: base(context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<Product>> GetAll(
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
			IQueryable<Product> query = _context.Products.Include(p => p.Images);

			if (latitude != null && longitude != null && radiusInKilometers != null)
			{
				Console.WriteLine("latitude: " + latitude);
				double earthRadius = 6371;
				double deltaLat = radiusInKilometers.Value / earthRadius * (180 / Math.PI);
				double deltaLon =
					radiusInKilometers.Value
					/ (earthRadius * Math.Cos(Math.PI * latitude.Value / 180))
					* (180 / Math.PI);

				double minLat = latitude.Value - deltaLat;
				double maxLat = latitude.Value + deltaLat;
				double minLon = longitude.Value - deltaLon;
				double maxLon = longitude.Value + deltaLon;

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

		public async Task<Product> GetById(string id)
		{
			return await _context
				.Products.Include(p => p.Brand)
				.Include(p => p.Images)
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Id == id);
		}
	}
}
