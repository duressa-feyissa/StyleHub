using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
	public class CategoryRepository(StyleHubDBContext context)
		: GenericRepository<Category>(context), ICategoryRepository
	{
		public async Task<IReadOnlyList<Category>> GetAll()
		{
			return await context.Categories.ToListAsync();
		}

		public async Task<Category> GetById(string id)
		{
			var user = await context.Categories.FirstOrDefaultAsync(u => u.Id == id);
			return user!;
		}

		public async Task<Category> GetByName(string name)
		{
			var brand = await context.Categories.FirstOrDefaultAsync(u => u.Name == name);
			return brand!;
		}
		
		 public async Task<IReadOnlyList<Category>> GetByIds(List<string> ids)
        {
            return await context.Categories.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

	}
}