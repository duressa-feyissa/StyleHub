namespace StyleHub.Application.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task<int> Save();
    }
}