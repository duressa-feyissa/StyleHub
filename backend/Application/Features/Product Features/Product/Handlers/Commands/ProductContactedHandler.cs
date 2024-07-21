using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Product.Requests.Commands;
using backend.Application.Response;
using backend.Domain.Entities.Product;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Commands;

public class ProductContactedHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<ProductContactedRequest, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(ProductContactedRequest request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.ContactedProductRepository.IsProductContactedAsync(
            new ContactedProduct
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
            }
        );

        if (!result)
        {
            throw new NotFoundException("Already contacted");
        }

        return new BaseResponse<string>
        {
            Message = "Product contacted successfully",
            Success = true,
            Data = $"Product with id {request.ProductId} contacted successfully"
        };
    }
}