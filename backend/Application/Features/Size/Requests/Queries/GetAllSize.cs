using Application.DTO.SizeDTO.DTO;
using MediatR;

namespace Application.Features.Size.Requests.Queries
{
    public class GetAllSize : IRequest<List<SizeResponseDTO>>
    {
    }
}
