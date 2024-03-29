using Application.DTO.Product.SizeDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Size.Requests.Queries
{
    public class GetAllSize : IRequest<List<SizeResponseDTO>>
    {
    }
}
