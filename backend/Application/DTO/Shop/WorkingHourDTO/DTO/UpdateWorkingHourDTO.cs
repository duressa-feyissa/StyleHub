namespace backend.Application.DTO.Shop.WorkingHourDTO.DTO;

public class UpdateWorkingHourDTO
{
    public required string Id { get; set; }
    public string Day { get; set; }
    public string Time { get; set; }
}