namespace backend.Application.DTO.Shop.ReviewDTO.DTO;

public class ReviewResponseDTO
{
    public string Id { get; set; }
    public string ShopId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Image { get; set; }
    public string Review { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}