using Application.DTO.Common.Location.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Common_Features.Location.Requests.Commands
{
    public class DeleteLocationRequest : IRequest<BaseResponse<LocationResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}