using backend.Application.DTO.Common.Image.DTO;
using MediatR;

namespace backend.Application.Features.Common_Features.Image.Requests.Queries
{
    public class GetImageByIdRequest : IRequest<ImageResponseDTO>
    {
        public required string Id { get; set; }
    }
}
