using Application.DTO.Product.MaterialDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Material.Requests.Commands
{
    public class CreateMaterialRequest : IRequest<BaseResponse<MaterialResponseDTO>>
    {
        public BaseMaterialDTO? Material { get; set; }
    }
}