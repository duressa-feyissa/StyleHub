using backend.Application.Contracts.Persistence.Repositories.Common;

namespace backend.Application.Contracts.Persistence.Repositories.User
{
	public interface IUserRepository : IGenericRepository<Domain.Entities.User.User>
	{
		Task<IReadOnlyList<Domain.Entities.User.User>> GetAll(
			int skip,
			int limit,
			string search,
			string sortBy,
			string orderBy,
			bool isVerified
		);
		Task<Domain.Entities.User.User> GetById(string id);
		Task<Domain.Entities.User.User> GetByPhoneNumber(string phoneNumber);
		Task<Domain.Entities.User.User> GetByEmail(string email);
		Task<bool> IsPhoneNumberRegistered(string phoneNumber);
	}
}
