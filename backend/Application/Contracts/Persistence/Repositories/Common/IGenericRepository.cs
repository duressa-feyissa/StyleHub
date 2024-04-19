namespace backend.Application.Contracts.Persistence.Repositories.Common
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}
