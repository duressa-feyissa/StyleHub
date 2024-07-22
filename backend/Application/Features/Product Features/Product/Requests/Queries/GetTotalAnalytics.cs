using backend.Application.DTO.Product.ProductDTO.DTO;

namespace backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

public class GetTotalAnalytics: IRequest<ProductAnalyticResponse>
{
    public string ProductId { get; set; }
}