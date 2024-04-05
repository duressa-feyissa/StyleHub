using Application.DTO.Common.Image.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Common_Features.Image.Requests.Commands
{
    public class UploadImageRequest : IRequest<BaseResponse<ImageResponseDTO>>
    {
        public required string UserId { get; set; }
        public required ImageUploadDTO Image { get; set; }
    }
}
