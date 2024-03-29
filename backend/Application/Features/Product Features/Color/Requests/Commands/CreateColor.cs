using Application.DTO.Product.ColorDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Color.Requests.Commands
{
    public class CreateColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public CreateColorDTO? Color { get; set; }
    }
}