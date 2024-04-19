using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Requests.Commands
{
    public class CreateSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public CreateSizeDTO? Size { get; set; }
    }
}