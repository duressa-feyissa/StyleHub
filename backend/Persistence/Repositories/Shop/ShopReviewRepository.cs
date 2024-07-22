using backend.Application.Contracts.Persistence.Repositories.Shop;
using backend.Application.Exceptions;
using backend.Domain.Entities.Shop;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Shop;

public class ShopReviewRepository(StyleHubDBContext context)
    : GenericRepository<ShopReview>(context), IShopReviewRepository
{
    public async Task<IReadOnlyList<ShopReview>> GetShopReviewListAsync(string shopId, string? userId, int? rating, string? sortBy, string? sortOrder, int skip = 0,
        int limit = 10)
    {
        IQueryable<ShopReview> query = context.ShopReviews.Include(p => p.User).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(shopId))
        {
            query = query.Where(r => r.ShopId == shopId);
        }
        if (!string.IsNullOrWhiteSpace(userId))
        {
            query = query.Where(r => r.UserId == userId);
        }

        if (rating != null)
        {
            query = query.Where(r => r.Rating == rating);
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = sortBy switch
            {
                "rating" => query.OrderBy(r => r.Rating),
                "createdAt" => query.OrderBy(r => r.CreatedAt),
                _ => query.OrderBy(r => r.CreatedAt)
            };
        }

        if (!string.IsNullOrWhiteSpace(sortOrder))
        {
            query = sortOrder switch
            {
                "asc" => query.OrderBy(r => r.CreatedAt),
                "desc" => query.OrderByDescending(r => r.CreatedAt),
                _ => query.OrderBy(r => r.CreatedAt)
            };
        }

        return await query.Skip(skip).Take(limit).ToListAsync();
    }

    public async Task<ShopReview> GetShopReviewByIdAsync(string id)
    {
        var shopReview = await context.ShopReviews.Include(p => p.User).FirstOrDefaultAsync(r => r.Id == id);
        if (shopReview == null)
        {
            throw new NotFoundException("Shop review not found");
        }
        return shopReview;
    }

    public async Task<bool> IsShopReviewOwnerAsync(string shopReviewId, string userId)
    {
        var shopReview = await context.ShopReviews.FirstOrDefaultAsync(r => r.Id == shopReviewId);
        if (shopReview == null)
        {
            throw new NotFoundException("Shop review not found");
        }
        return shopReview.UserId == userId;
    }

    public async Task<double> GetShopRatingAsync(string shopId)
    {
        var shopReviews = await context.ShopReviews.Where(r => r.ShopId == shopId).ToListAsync();
        if (shopReviews.Count == 0)
        {
            return 0;
        }
        return shopReviews.Average(r => r.Rating);
    }

    public async Task<int> GetShopReviewCountAsync(string shopId)
    {
        return await context.ShopReviews.CountAsync(r => r.ShopId == shopId);
    }
}