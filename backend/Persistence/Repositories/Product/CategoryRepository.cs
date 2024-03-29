using Application.Contracts.Persistence.Repositories.Product;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Repositories.Common;

namespace Persistence.Repositories.Product
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		StyleHubDBContext _context;
		public CategoryRepository(StyleHubDBContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<Category>> GetAll()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category> GetById(string id)
		{
			var user = await _context.Categories.FirstOrDefaultAsync(u => u.Id == id);
			return user!;
		}

		public async Task<Category> GetByName(string name)
		{
			var brand = await _context.Categories.FirstOrDefaultAsync(u => u.Name == name);
			return brand!;
		}
		
		 public async Task<IReadOnlyList<Category>> GetByIds(List<string> ids)
        {
            return await _context.Categories.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

	}
}