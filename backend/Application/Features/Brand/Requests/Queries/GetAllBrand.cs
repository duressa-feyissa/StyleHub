using Application.DTO.BrandDTO.DTO;
using MediatR;

namespace Application.Features.Brand.Requests.Queries
{
    public class GetAllBrand : IRequest<List<BrandResponseDTO>>
    {
    }
}
