using Application.Contracts.Persistence.Repositories.Common;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence.Repositories.Common
{
	public class ImageRepository : IImageRepository
	{
		StyleHubDBContext _context;

		public ImageRepository(StyleHubDBContext context)
		{
			_context = context;
		}

		public async Task<Image> Add(Image entity)
		{
			var result = await _context.Images.AddAsync(entity);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Image> Delete(Image entity)
		{
			var result = _context.Images.Remove(entity);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<IReadOnlyList<Image>> GetAll(string userId)
		{
			return await _context.Images.Where(i => i.User.Id == userId).ToListAsync();
		}

		public async Task<Image> GetById(string id)
		{
			var result = await  _context.Images.FirstOrDefaultAsync(i => i.Id == id);
			return result!;
		}
	}
}
