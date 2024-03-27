using Application.DTO.LocationDTO.DTO;
using MediatR;

namespace Application.Features.Location.Requests.Queries
{
    public class GetAllLocation : IRequest<List<LocationResponseDTO>>
    {
    }
}
