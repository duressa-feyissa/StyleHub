using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.DTO.Shop.ReviewDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Review.Requests.Commands;
using backend.Application.Response;
using backend.Domain.Entities.Shop;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Handlers.Commands;

public class CreateReviewHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<CreateReviewRequest, BaseResponse<ReviewResponseDTO>>
{
    public async Task<BaseResponse<ReviewResponseDTO>> Handle(CreateReviewRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateReviewValidation(unitOfWork.ShopRepository);
        var validationResult = await validator.ValidateAsync(request.Review);

        if (!validationResult.IsValid)
            throw new BadRequestException(
                validationResult.Errors.FirstOrDefault()?.ErrorMessage!
            );

        var review = mapper.Map<ShopReview>(request.Review);
        review.UserId = request.UserId;
        await unitOfWork.ShopReviewRepository.Add(review);
        review = await unitOfWork.ShopReviewRepository.GetShopReviewByIdAsync(review.Id);
        var reviewResponse = mapper.Map<ReviewResponseDTO>(review);

        return new BaseResponse<ReviewResponseDTO>
        {
            Success = true,
            Data = reviewResponse,
            Message = "Review Created Successfully"
        };
    }
}