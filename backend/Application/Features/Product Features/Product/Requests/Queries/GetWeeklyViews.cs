using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Queries;

public class GetWeeklyViews: IRequest<Dictionary<string, int>>
{
    public string ProductId { get; set; }
}