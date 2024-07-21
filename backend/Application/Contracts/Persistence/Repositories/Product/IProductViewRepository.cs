using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product;

public interface IProductViewRepository : IGenericRepository<ProductView>
{
    Task<int> GetTodayViewCountAsync(string productId);
    Task<int> GetTotalViewCountAsync(string productId);
    Task<Dictionary<string, int>> GetViewCountByWeekAsync(string productId);
    Task<Dictionary<int, int>> GetViewCountByMonthAsync(string productId);
    Task<Dictionary<string, int>> GetViewCountByYearAsync(string productId);
}