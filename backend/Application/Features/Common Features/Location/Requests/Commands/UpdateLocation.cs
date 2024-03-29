using Application.DTO.Common.Location.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Common_Features.Location.Requests.Commands
{
    public class UpdateLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateLocationDTO? Location { get; set; }
    }
}