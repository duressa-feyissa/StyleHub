using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Shop;

namespace backend.Application.Contracts.Persistence.Repositories.Shop;

public interface IShopReviewRepository: IGenericRepository<ShopReview>
{
    Task<IReadOnlyList<ShopReview>> GetShopReviewListAsync(
        string shopId,
        string? userId,
        int? rating,
        string? sortBy,
        string? sortOrder,
        int skip = 0,
        int limit = 10
        );
    Task<ShopReview> GetShopReviewByIdAsync(string id);
    Task<bool> IsShopReviewOwnerAsync(string shopReviewId, string userId);
    Task<double> GetShopRatingAsync(string shopId);
    Task<int> GetShopReviewCountAsync(string shopId);
}