using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Common;
using backend.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Common
{
	public class LocationRepository(StyleHubDBContext context)
		: GenericRepository<Location>(context), ILocationRepository
	{
		public async Task<IReadOnlyList<Location>> GetAll()
		{
			return await context.Locations.ToListAsync();
		}

		public async Task<Location> GetById(string id)
		{
			var location = await context.Locations.FirstOrDefaultAsync(u => u.Id == id);
			return location!;
		}

		public async Task<Location> GetByName(string name)
		{
			var location = await context.Locations.FirstOrDefaultAsync(u => u.Name == name);
			return location!;
		}
		
		public async Task<Location> GetByCoordinates(double latitude, double longitude)
		{
			var location = await context.Locations.FirstOrDefaultAsync(u => u.Latitude == latitude && u.Longitude == longitude);
			return location!;
		}

	}
}