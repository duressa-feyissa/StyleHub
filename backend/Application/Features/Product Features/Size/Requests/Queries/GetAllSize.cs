using backend.Application.DTO.Product.SizeDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Requests.Queries
{
    public class GetAllSize : IRequest<List<SizeResponseDTO>>
    {
    }
}
