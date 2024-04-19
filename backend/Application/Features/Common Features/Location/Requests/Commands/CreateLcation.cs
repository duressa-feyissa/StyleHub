using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Requests.Commands
{
    public class CreateLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public CreateLocationDTO? Location { get; set; }
    }
}
