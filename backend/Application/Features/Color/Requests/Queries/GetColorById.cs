using Application.DTO.ColorDTO.DTO;
using MediatR;

namespace Application.Features.Color.Requests.Queries
{
    public class GetColorById : IRequest<ColorResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}