using Application.DTO.LocationDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Location.Requests.Commands
{
    public class CreateLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public BaseLocationDTO? Location { get; set; }
    }
}