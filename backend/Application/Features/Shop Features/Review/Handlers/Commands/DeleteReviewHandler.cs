using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Review.Requests.Commands;
using backend.Application.Response;
using MediatR;


public class DeleteReviewHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<DeleteReviewRequest, BaseResponse<ReviewResponseDTO>>
{
    public async Task<BaseResponse<ReviewResponseDTO>> Handle(DeleteReviewRequest request, CancellationToken cancellationToken)
    {
        var review = await unitOfWork.ShopReviewRepository.GetShopReviewByIdAsync(request.Id);
        if (review == null)
            throw new NotFoundException("Review is not found");
        
        if (review.UserId != request.UserId)
            throw new BadRequestException("You are not allowed to delete this review");
        
        await unitOfWork.ShopReviewRepository.Delete(review);
        return new BaseResponse<ReviewResponseDTO>
        {
            Success = true,
            Message = "Review Deleted Successfully",
            Data = mapper.Map<ReviewResponseDTO>(review)
        };
    }
}