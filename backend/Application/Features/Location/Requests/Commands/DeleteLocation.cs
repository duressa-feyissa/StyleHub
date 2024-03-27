using Application.DTO.LocationDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Location.Requests.Commands
{
    public class DeleteLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}