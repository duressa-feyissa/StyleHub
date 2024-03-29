using Application.DTO.Product.BrandDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Brand.Requests.Commands
{
    public class UpdateBrandRequest : IRequest<BaseResponse<BrandResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateBrandDTO? Brand { get; set; }
    }
}
