using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Common;
using backend.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Common
{
	public class ImageRepository(StyleHubDBContext context) : IImageRepository
	{
		public async Task<Image> Add(Image entity)
		{
			var result = await context.Images.AddAsync(entity);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Image> Delete(Image entity)
		{
			var result = context.Images.Remove(entity);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<IReadOnlyList<Image>> GetAll(string userId)
		{
			return await context.Images.Where(i => i.User.Id == userId).ToListAsync();
		}

		public async Task<IReadOnlyList<Image>> GetByIds(List<string> ids)
		{
			return await context.Images.Where(i => ids.Contains(i.Id)).ToListAsync();
		}

		public async Task<Image> GetById(string id)
		{
			var result = await  context.Images.FirstOrDefaultAsync(i => i.Id == id);
			return result!;
		}
	}
}
