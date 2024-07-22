using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries;

public class GetWeeklyViewsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetWeeklyViews, Dictionary<string, int>> {
    
    public async Task<Dictionary<string, int>> Handle(GetWeeklyViews request, CancellationToken cancellationToken)
    {
        return await unitOfWork.ProductViewRepository.GetViewCountByWeekAsync(request.ProductId);
    }
}