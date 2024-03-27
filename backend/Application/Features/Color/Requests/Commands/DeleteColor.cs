using Application.DTO.ColorDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Color.Requests.Commands
{
    public class DeleteColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}