using Application.DTO.Product.BrandDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Brand.Requests.Commands
{
    public class CreateBrandRequest : IRequest<BaseResponse<BrandResponseDTO>>
    {
        public CreateBrandDTO? Brand { get; set; }
    }
}