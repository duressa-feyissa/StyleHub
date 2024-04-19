using backend.Domain.Entities.Common;

namespace backend.Application.Contracts.Persistence.Repositories.Common
{
    public interface IImageRepository
    {
        Task<Image> Add(Image entity);
        Task<Image> Delete(Image entity);
        Task<IReadOnlyList<Image>> GetAll(string userId);
        Task<IReadOnlyList<Image>> GetByIds(List<string> ids);
        Task<Image> GetById(string id);
    }
}
