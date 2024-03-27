using Application.DTO.ColorDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Color.Requests.Commands
{
    public class CreateColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public BaseColorDTO? Color { get; set; }
    }
}