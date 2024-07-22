using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;

namespace backend.Application.Contracts.Persistence.Repositories.Product;

public interface IFavouriteProductRepository
{
    Task<IReadOnlyList<Domain.Entities.Product.Product>> GetAll(
        string userId,
        int skip = 0,
        int limit = 10
    );
    
    Task<bool> AddOrRemove(string userId, string productId);
    
    Task<bool> IsFavourite(string userId, string productId);
    
    Task<int> Count(string productId);
    
}