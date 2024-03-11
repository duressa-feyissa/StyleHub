using SocialSync.Persistence.Repositories;
using StyleHub.Application.Contracts;

namespace StyleHub.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StyleHubDBContext _context;
        private IUserRepository _userRepository;

        public UnitOfWork(StyleHubDBContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }

        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}