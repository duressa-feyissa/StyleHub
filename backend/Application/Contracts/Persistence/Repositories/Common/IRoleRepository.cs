using backend.Domain.Entities.Common;

namespace backend.Application.Contracts.Persistence.Repositories.Common
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		Task<IReadOnlyList<Role>> GetAll();
		Task<Role> GetById(string id);

		Task<Role> GetByName(string name);

		Task<Role> GetByCode(string code);
	}
}