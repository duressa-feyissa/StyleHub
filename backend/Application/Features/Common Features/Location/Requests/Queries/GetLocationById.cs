using Application.DTO.Common.Location.DTO;
using MediatR;

namespace Application.Features.Common_Features.Location.Requests.Queries
{
    public class GetLocationById : IRequest<LocationResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}
