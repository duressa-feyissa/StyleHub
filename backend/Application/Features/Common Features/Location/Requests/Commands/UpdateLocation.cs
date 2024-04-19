using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Requests.Commands
{
    public class UpdateLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateLocationDTO? Location { get; set; }
    }
}