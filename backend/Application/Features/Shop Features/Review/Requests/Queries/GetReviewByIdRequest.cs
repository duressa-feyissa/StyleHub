using backend.Application.DTO.Shop.ReviewDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Requests.Queries;

public class GetReviewByIdRequest: IRequest<ReviewResponseDTO>
{
    public required string Id { get; set; }
}