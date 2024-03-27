using Application.DTO.BrandDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Brand.Requests.Commands
{
    public class CreateBrandRequest : IRequest<BaseResponse<BrandResponseDTO>>
    {
        public BaseBrandDTO? Brand { get; set; }
    }
}