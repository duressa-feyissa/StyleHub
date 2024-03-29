using Application.DTO.Product.MaterialDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Material.Requests.Commands
{
    public class UpdateMaterialRequest : IRequest<BaseResponse<MaterialResponseDTO>>
    {
        public string Id { get; set; } = "";
        public BaseMaterialDTO? Material { get; set; }
    }
}