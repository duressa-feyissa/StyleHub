using Application.DTO.Common.Image.DTO;
using MediatR;

namespace Application.Features.Common_Features.Image.Requests.Queries
{
    public class GetAllImageByUserIdRequest : IRequest<List<ImageResponseDTO>>
    {
        public required string UserId { get; set; }
    }
}
