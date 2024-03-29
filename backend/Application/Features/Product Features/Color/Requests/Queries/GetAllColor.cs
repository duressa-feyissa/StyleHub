using Application.DTO.Product.ColorDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Color.Requests.Queries
{
    public class GetAllColor : IRequest<List<ColorResponseDTO>>
    {
    }
}
