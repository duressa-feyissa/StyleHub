using Application.DTO.Product.SizeDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Size.Requests.Commands
{
    public class CreateSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public CreateSizeDTO? Size { get; set; }
    }
}