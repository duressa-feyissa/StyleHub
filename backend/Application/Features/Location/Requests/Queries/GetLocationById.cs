using Application.DTO.LocationDTO.DTO;
using MediatR;

namespace Application.Features.Location.Requests.Queries
{
    public class GetLocationById : IRequest<LocationResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}