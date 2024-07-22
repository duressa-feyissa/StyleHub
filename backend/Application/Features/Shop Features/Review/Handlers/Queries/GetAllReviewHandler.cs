using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.Features.Shop_Features.Review.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Handlers.Queries;

public class GetAllReviewHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<GetAllReviewRequest, List<ReviewResponseDTO>>
{
    public async Task<List<ReviewResponseDTO>> Handle(GetAllReviewRequest request, CancellationToken cancellationToken)
    {
        var reviews = await unitOfWork.ShopReviewRepository.GetShopReviewListAsync(
            shopId: request.ShopId,
            userId: request.UserId,
            sortBy: request.SortBy,
            sortOrder: request.SortOrder,
            rating: request.Rating,
            skip: request.Skip,
            limit: request.Limit
        );
        return mapper.Map<List<ReviewResponseDTO>>(reviews);
    }
}