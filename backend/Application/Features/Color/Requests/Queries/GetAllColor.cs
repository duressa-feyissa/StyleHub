using Application.DTO.ColorDTO.DTO;
using MediatR;

namespace Application.Features.Color.Requests.Queries
{
    public class GetAllColor : IRequest<List<ColorResponseDTO>>
    {
    }
}
