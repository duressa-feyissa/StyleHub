namespace backend.Application.DTO.Product.ProductDTO.DTO;

public class ProductAnalyticResponse
{
    public int TotalViews { get; set; }
    public int TodayViews { get; set; }
    public int TotalFavorites { get; set; }
    public int TotalContacted { get; set; }
    public Dictionary<int, int> ThisMonthViews { get; set; }
    public Dictionary<string, int> ThisWeekViews { get; set; }
    public Dictionary<string, int> ThisYearViews { get; set; }
}