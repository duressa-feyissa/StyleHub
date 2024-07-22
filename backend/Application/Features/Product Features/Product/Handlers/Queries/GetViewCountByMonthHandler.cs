using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries;

public class GetViewCountByMonthHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetViewCountByMonth, Dictionary<int, int>>
{
    public async Task<Dictionary<int, int>> Handle(GetViewCountByMonth request, CancellationToken cancellationToken)
    {
        return await unitOfWork.ProductViewRepository.GetViewCountByMonthAsync(request.ProductId);
    }
    
}