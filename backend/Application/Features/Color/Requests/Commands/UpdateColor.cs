using Application.DTO.ColorDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Color.Requests.Commands
{
    public class UpdateColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public string Id { get; set; } = "";
        public BaseColorDTO? Color { get; set; }
    }
}