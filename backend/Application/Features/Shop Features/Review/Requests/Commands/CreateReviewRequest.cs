using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Requests.Commands;

public class CreateReviewRequest: IRequest<BaseResponse<ReviewResponseDTO>>
{
    public required CreateReviewDTO Review { get; set; }
    public required string UserId { get; set; }
}