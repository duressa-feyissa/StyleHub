namespace Application.Contracts
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        Task<int> Save();
    }
}