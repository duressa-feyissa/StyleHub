using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Review.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.Review.Handlers.Queries;

public class GetReviewByIdHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<GetReviewByIdRequest, ReviewResponseDTO>
{
    public async Task<ReviewResponseDTO> Handle(GetReviewByIdRequest request, CancellationToken cancellationToken)
    {
        var review = await unitOfWork.ShopReviewRepository.GetShopReviewByIdAsync(request.Id);
        if (review == null)
            throw new NotFoundException("Review is not found");
        return mapper.Map<ReviewResponseDTO>(review);
    }
}