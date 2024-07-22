using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Shop;

namespace backend.Application.Contracts.Persistence.Repositories.Shop;

public interface IWorkingHourRepository: IGenericRepository<WorkingHour>
{
    Task<WorkingHour> GetWorkingHourByIdAsync(string id);
    
    Task<IReadOnlyList<WorkingHour>> GetWorkingHoursByShopIdAsync(string shopId);
    
    Task<bool> IsWorkingHourDayTimeExistsAsync(string shopId, string day);
    
    Task<bool> IsExistingWorkingHourAsync(string id);
}