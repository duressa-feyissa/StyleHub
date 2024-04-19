using backend.Application.DTO.Common.Location.DTO;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Requests.Queries
{
    public class GetAllLocation : IRequest<List<LocationResponseDTO>>
    {
    }
}
