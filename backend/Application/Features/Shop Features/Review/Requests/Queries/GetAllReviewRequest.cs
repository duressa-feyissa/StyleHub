using backend.Application.DTO.Shop.ReviewDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Requests.Queries;

public class GetAllReviewRequest: IRequest<List<ReviewResponseDTO>>
{
    public required string ShopId { get; set; }
    public string? UserId { get; set; }
    public int? Rating { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;
}