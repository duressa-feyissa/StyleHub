using backend.Application.DTO.Product.BrandDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Requests.Queries
{
    public class GetAllBrand : IRequest<List<BrandResponseDTO>>
    {
    }
}
