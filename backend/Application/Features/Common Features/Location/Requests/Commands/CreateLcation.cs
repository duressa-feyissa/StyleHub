using Application.DTO.Common.Location.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Common_Features.Location.Requests.Commands
{
    public class CreateLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public CreateLocationDTO? Location { get; set; }
    }
}
