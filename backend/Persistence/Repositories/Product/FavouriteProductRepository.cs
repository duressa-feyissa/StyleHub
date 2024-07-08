using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product
{
    public class FavouriteProductRepository(StyleHubDBContext context)
        : IFavouriteProductRepository
    {
        public async Task<IReadOnlyList<Domain.Entities.Product.Product>> GetAll(string userId, int skip = 0, int limit = 10)
        {
            var favouriteProducts = await context.FavouriteProducts
                .Where(fp => fp.UserId == userId)
                .Skip(skip)
                .Take(limit)
                .Include(fp => fp.Product) // Ensure the Product is included
                .Select(fp => fp.Product)
                .ToListAsync();

            return favouriteProducts;
        }
        
        public async Task<bool> AddOrRemove(string userId, string productId)
        {
            var favouriteProduct = await context.FavouriteProducts
                .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.ProductId == productId);

            if (favouriteProduct == null)
            {
                favouriteProduct = new FavouriteProduct
                {
                    UserId = userId,
                    ProductId = productId
                };
                await context.FavouriteProducts.AddAsync(favouriteProduct);
            }
            else
            {
                context.FavouriteProducts.Remove(favouriteProduct);
            }

            await context.SaveChangesAsync();

            return true;
        }
    }
}