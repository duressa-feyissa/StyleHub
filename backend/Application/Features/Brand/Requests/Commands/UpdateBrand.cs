using Application.DTO.BrandDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Brand.Requests.Commands
{
    public class UpdateBrandRequest : IRequest<BaseResponse<BrandResponseDTO>>
    {
        public string Id { get; set; } = "";
        public BaseBrandDTO? Brand { get; set; }
    }
}