using backend.Application.DTO.Product.DesignDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Requests.Queries
{
    public class GetAllDesign : IRequest<List<DesignResponseDTO>>
    {
    }
}
