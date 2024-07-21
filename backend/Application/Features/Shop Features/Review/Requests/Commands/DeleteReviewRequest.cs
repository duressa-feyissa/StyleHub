using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Requests.Commands;

public class DeleteReviewRequest: IRequest<BaseResponse<ReviewResponseDTO>>
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
}