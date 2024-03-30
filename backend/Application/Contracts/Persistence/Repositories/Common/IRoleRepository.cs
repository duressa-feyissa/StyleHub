using Domain.Entities.Common;

namespace Application.Contracts.Persistence.Repositories.Common
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		Task<IReadOnlyList<Role>> GetAll();
		Task<Role> GetById(string id);

		Task<Role> GetByName(string name);

		Task<Role> GetByCode(string code);
	}
}