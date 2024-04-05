using Application.DTO.Common.Image.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Common_Features.Image.Requests.Commands
{
    public class DeleteImageRequest : IRequest<BaseResponse<ImageResponseDTO>>
    {
        public required string Id { get; set; }
    }
}
