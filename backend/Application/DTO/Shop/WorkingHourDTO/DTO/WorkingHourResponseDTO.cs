namespace backend.Application.DTO.Shop.WorkingHourDTO.DTO;

public class WorkingHourResponseDTO
{
    public required string Id { get; set; }
    public required string ShopId { get; set; }
    public required string Day { get; set; }
    public required string Time { get; set; }
}