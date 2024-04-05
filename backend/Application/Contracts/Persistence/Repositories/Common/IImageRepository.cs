using Domain.Entities.Common;

namespace Application.Contracts.Persistence.Repositories.Common
{
    public interface IImageRepository
    {
        Task<Image> Add(Image entity);
        Task<Image> Delete(Image entity);
        Task<IReadOnlyList<Image>> GetAll(string userId);
        Task<Image> GetById(string id);
    }
}
