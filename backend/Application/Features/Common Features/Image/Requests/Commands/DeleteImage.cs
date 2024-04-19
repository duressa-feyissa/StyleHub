using backend.Application.DTO.Common.Image.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Image.Requests.Commands
{
    public class DeleteImageRequest : IRequest<BaseResponse<ImageResponseDTO>>
    {
        public required string Id { get; set; }
    }
}
