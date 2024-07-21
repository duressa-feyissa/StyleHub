using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Domain.Entities.Common;
namespace backend.Application.Contracts.Persistence.Repositories.Shop;

public interface IShopRepository: IGenericRepository<Domain.Entities.Shop.Shop>
{
    Task<IReadOnlyList<Domain.Entities.Shop.Shop>> GetShopListAsync(
        string? search,
        List<string>? categories,
        List<WorkingHourQueryParamater>? workingHours,
        double? radiusInKilometers,
        double? latitude,
        double? longitude,
        int? rating,
        bool? verified,
        bool? active,
        string? ownerId,
        string? sortBy,
        string? sortOrder,
        int skip = 0,
        int limit = 10
        );
    Task<Domain.Entities.Shop.Shop> GetShopByIdAsync(string id);
    Task<bool> IsShopNameUniqueAsync(string name);
    Task<bool> IsShopOwnerAsync(string shopId, string userId);
    Task<bool> IsShopVerifiedAsync(string shopId);
    Task<bool> IsShopActiveAsync(string shopId);
    Task<bool> IsUserShopOwnerAsync(string userId);
    Task<bool> ExistsAsync(string shopId);
    Task<IReadOnlyList<Image>> GetShopImagesAsync(string shopId,int skip = 0,
        int limit = 10);
    Task<IReadOnlyList<string>> GetShopVideosAsync(string shopId,int skip = 0,
        int limit = 10);
    Task<string> FollowShopAsync(string shopId, string userId);
    Task<IReadOnlyList<Domain.Entities.Shop.Shop>> GetShopFollowedUsersAsync(string userId,int skip = 0,
        int limit = 10);
    Task<IReadOnlyList<Domain.Entities.Product.Product>> GetShopProductsAsync(string userId,int skip = 0,
        int limit = 10);
    
    Task<int> GetShopFollowersCountAsync(string shopId);
    Task<int> GetShopProductsCountAsync(string shopId);
    Task<int> GetShopReviewsCountAsync(string shopId);
    Task<int> GetShopFavoritesCountAsync(string shopId);
    Task<int> GetShopViewsCountAsync(string shopId);
    Task<int> GetShopProductsContactedCountAsync(string shopId);
    Task<double> GetShopAverageRatingAsync(string shopId);
    Task<bool> IsShopFollowedByUserAsync(string shopId, string userId);
}