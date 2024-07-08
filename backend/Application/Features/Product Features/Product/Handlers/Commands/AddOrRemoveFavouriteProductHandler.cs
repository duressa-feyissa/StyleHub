using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.Features.Product_Features.Product.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Commands;

public class AddOrRemoveFavouriteProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<AddOrRemoveFavouriteProduct, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(AddOrRemoveFavouriteProduct request, CancellationToken cancellationToken)
    {
        await unitOfWork.FavouriteProductRepository.AddOrRemove(
            request.UserId,
            request.ProductId
        );
        
        return new BaseResponse<string>
        {
            Message = "Product added or removed from favourite successfully",
            Success = true,
            Data = request.ProductId
        };
            
    }
}