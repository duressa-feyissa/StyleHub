using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence.Repositories.Common
{
	public class LocationRepository : GenericRepository<Location>, ILocationRepository
	{
		StyleHubDBContext _context;
		public LocationRepository(StyleHubDBContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<Location>> GetAll()
		{
			return await _context.Locations.ToListAsync();
		}

		public async Task<Location> GetById(string id)
		{
			var location = await _context.Locations.FirstOrDefaultAsync(u => u.Id == id);
			return location!;
		}

		public async Task<Location> GetByName(string name)
		{
			var location = await _context.Locations.FirstOrDefaultAsync(u => u.Name == name);
			return location!;
		}
		
		public async Task<Location> GetByCoordinates(double latitude, double longitude)
		{
			var location = await _context.Locations.FirstOrDefaultAsync(u => u.Latitude == latitude && u.Longitude == longitude);
			return location!;
		}

	}
}