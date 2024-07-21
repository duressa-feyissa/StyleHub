using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product;

public class ProductViewRepository(StyleHubDBContext context)
    : GenericRepository<ProductView>(context), IProductViewRepository
{
    public async Task<int> GetTodayViewCountAsync(string productId)
    {
        var today = DateTime.UtcNow.Date;
        return await context.ProductViews
            .CountAsync(pv => pv.ProductId == productId && pv.ViewedAt >= today);
    }

    public async Task<int> GetTotalViewCountAsync(string productId)
    {
        return await context.ProductViews
            .CountAsync(pv => pv.ProductId == productId);
    }

    public async Task<Dictionary<string, int>> GetViewCountByWeekAsync(string productId)
    {
        var startOfWeek = DateTime.UtcNow.StartOfWeek(DayOfWeek.Sunday);
        var endOfWeek = startOfWeek.AddDays(7);

        var data = await context.ProductViews
            .Where(pv => pv.ProductId == productId && pv.ViewedAt >= startOfWeek && pv.ViewedAt < endOfWeek)
            .GroupBy(pv => pv.ViewedAt.DayOfWeek)
            .Select(g => new
            {
                Day = g.Key,
                ViewCount = g.Count()
            })
            .ToListAsync();

        var result = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>()
            .ToDictionary(day => day.ToString(), day => data.FirstOrDefault(d => d.Day == day)?.ViewCount ?? 0);

        return result;
    }
    
    public async Task<Dictionary<int, int>> GetViewCountByMonthAsync(string productId)
    {
        var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1);

        var data = await context.ProductViews
            .Where(pv => pv.ProductId == productId && pv.ViewedAt >= startOfMonth && pv.ViewedAt < endOfMonth)
            .GroupBy(pv => pv.ViewedAt.Day)
            .Select(g => new
            {
                Day = g.Key,
                ViewCount = g.Count()
            })
            .ToListAsync();

        var result = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month))
            .ToDictionary(day => day, day => data.FirstOrDefault(d => d.Day == day)?.ViewCount ?? 0);

        return result;
    }
    
    public async Task<Dictionary<string, int>> GetViewCountByYearAsync(string productId)
    {
        var startOfYear = new DateTime(DateTime.UtcNow.Year, 1, 1);
        var endOfYear = startOfYear.AddYears(1);

        var data = await context.ProductViews
            .Where(pv => pv.ProductId == productId && pv.ViewedAt >= startOfYear && pv.ViewedAt < endOfYear)
            .GroupBy(pv => pv.ViewedAt.Month)
            .Select(g => new
            {
                Month = g.Key,
                ViewCount = g.Count()
            })
            .ToListAsync();

        var months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        var result = Enumerable.Range(1, 12)
            .ToDictionary(month => months[month - 1], month => data.FirstOrDefault(d => d.Month == month)?.ViewCount ?? 0);

        return result;
    }


}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}
