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
            var favouriteProductsQuery = context.FavouriteProducts
                .Where(fp => fp.UserId == userId)
                .Skip(skip)
                .Take(limit)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.ProductImages)
                .ThenInclude(pi => pi.Image)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.ProductDesigns)
                .ThenInclude(pd => pd.Design)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.ProductBrands)
                .ThenInclude(pb => pb.Brand)
                .Include(fp => fp.Product)
                .ThenInclude(p => p.Shop)
                .AsSplitQuery()
                .AsNoTracking();

            var favouriteProducts = await favouriteProductsQuery
                .Select(fp => fp.Product)
                .OrderByDescending(fp => fp.CreatedAt)
                .ToListAsync();

            return favouriteProducts;
        }
        
        public async Task<bool> IsFavourite(string userId, string productId)
        {
            return await context.FavouriteProducts
                .AnyAsync(fp => fp.UserId == userId && fp.ProductId == productId);
        }

        public async Task<int> Count(string productId)
        {
            return await context.FavouriteProducts
                .CountAsync(fp => fp.ProductId == productId);
        }
        
        public async Task<bool> AddOrRemove(string userId, string productId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(productId))
            {
                return false;
            }
            
            if (!await context.Products.AnyAsync(p => p.Id == productId))
            {
                return false;
            }
            
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