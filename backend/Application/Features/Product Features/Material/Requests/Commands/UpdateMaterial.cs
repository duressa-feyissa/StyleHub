using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Material.Requests.Commands
{
    public class UpdateMaterialRequest : IRequest<BaseResponse<MaterialResponseDTO>>
    {
        public string Id { get; set; } = "";
        public BaseMaterialDTO? Material { get; set; }
    }
}