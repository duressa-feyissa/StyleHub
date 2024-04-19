using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Requests.Commands
{
    public class DeleteLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}