using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Queries;

public class GetViewCountByMonth: IRequest<Dictionary<int, int>>
{
    public string ProductId { get; set; }
}