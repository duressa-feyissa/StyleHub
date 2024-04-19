using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Requests.Commands
{
    public class UpdateBrandRequest : IRequest<BaseResponse<BrandResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateBrandDTO? Brand { get; set; }
    }
}
