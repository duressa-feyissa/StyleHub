using backend.Application.DTO.Common.Location.DTO;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Requests.Queries
{
    public class GetLocationById : IRequest<LocationResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}
