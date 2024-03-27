using Application.DTO.BrandDTO.DTO;
using MediatR;

namespace Application.Features.Brand.Requests.Queries
{
    public class GetBrandById : IRequest<BrandResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}