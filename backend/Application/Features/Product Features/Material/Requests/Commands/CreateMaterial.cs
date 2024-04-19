using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Material.Requests.Commands
{
    public class CreateMaterialRequest : IRequest<BaseResponse<MaterialResponseDTO>>
    {
        public BaseMaterialDTO? Material { get; set; }
    }
}