using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Requests.Commands
{
    public class DeleteBrandRequest : IRequest<BaseResponse<BrandResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}