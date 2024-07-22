using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries;

public class GetViewCountByYearHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetViewCountByYear, Dictionary<string, int>>
{
    public async Task<Dictionary<string, int>> Handle(GetViewCountByYear request, CancellationToken cancellationToken)
    {
        return await unitOfWork.ProductViewRepository.GetViewCountByYearAsync(request.ProductId);
    }
}