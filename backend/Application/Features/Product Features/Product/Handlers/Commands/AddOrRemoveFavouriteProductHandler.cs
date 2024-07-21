using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Product.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Commands;

public class AddOrRemoveFavouriteProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<AddOrRemoveFavouriteProduct, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(AddOrRemoveFavouriteProduct request, CancellationToken cancellationToken)
    {
      var result =  await unitOfWork.FavouriteProductRepository.AddOrRemove(
            request.UserId,
            request.ProductId
        );
      
        if (!result)
        {
           throw new NotFoundException("Product Not Found");
        }
        
        return new BaseResponse<string>
        {
            Message = "Product added or removed from favourite successfully",
            Success = true,
            Data = $"Product with id {request.ProductId} added or removed from favourite successfully"
        };
    }
}