namespace backend.Application.DTO.Shop.ReviewDTO.DTO;

public class CreateReviewDTO
{
    public string ShopId { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; }
}