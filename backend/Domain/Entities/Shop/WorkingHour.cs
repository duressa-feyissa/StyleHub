namespace backend.Domain.Entities.Shop;

public class WorkingHour
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Sunday { get; set; }
    public string? Monday { get; set; }
    public string? Tuesday { get; set; }
    public string? Wednesday { get; set; }
    public string? Thursday { get; set; }
    public string? Friday { get; set; }
    public string? Saturday { get; set; }
}