
using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Review.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Handlers.Commands;

public class UpdateReviewHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<UpdateReviewRequest, BaseResponse<ReviewResponseDTO>>
{
    public async Task<BaseResponse<ReviewResponseDTO>> Handle(UpdateReviewRequest request, CancellationToken cancellationToken)
    {
        var review = await unitOfWork.ShopReviewRepository.GetShopReviewByIdAsync(request.Review.Id);
        if (review == null)
            throw new NotFoundException("Review is not found");
        
        if (review.UserId != request.UserId)
            throw new BadRequestException("You are not allowed to update this review");
        if (request.Review.Rating < 1 || request.Review.Rating > 5)
            throw new BadRequestException("Rating must be between 1 and 5");
        review.Rating = request.Review.Rating;
        review.Review = request.Review.Comment;
        review = await unitOfWork.ShopReviewRepository.Update(review);
        var reviewResponse = mapper.Map<ReviewResponseDTO>(review);
        return new BaseResponse<ReviewResponseDTO>
        {
            Success = true,
            Data = reviewResponse,
            Message = "Review Updated Successfully"
        };
    }
}