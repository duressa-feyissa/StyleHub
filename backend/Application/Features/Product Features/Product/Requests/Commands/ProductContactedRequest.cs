using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Commands;

public class ProductContactedRequest: IRequest<BaseResponse<string>>
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
}