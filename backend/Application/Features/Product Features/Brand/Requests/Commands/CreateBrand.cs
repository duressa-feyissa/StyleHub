using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Requests.Commands
{
    public class CreateBrandRequest : IRequest<BaseResponse<BrandResponseDTO>>
    {
        public CreateBrandDTO? Brand { get; set; }
    }
}