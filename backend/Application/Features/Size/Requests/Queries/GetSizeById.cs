using Application.DTO.SizeDTO.DTO;
using MediatR;

namespace Application.Features.Size.Requests.Queries
{
    public class GetSizeById : IRequest<SizeResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}