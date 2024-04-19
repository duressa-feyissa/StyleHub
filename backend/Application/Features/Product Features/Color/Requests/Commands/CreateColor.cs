using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Requests.Commands
{
    public class CreateColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public CreateColorDTO? Color { get; set; }
    }
}