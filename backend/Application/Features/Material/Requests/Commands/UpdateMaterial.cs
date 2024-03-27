using Application.DTO.MaterialDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Material.Requests.Commands
{
    public class UpdateMaterialRequest : IRequest<BaseResponse<MaterialResponseDTO>>
    {
        public string Id { get; set; } = "";
        public BaseMaterialDTO? Material { get; set; }
    }
}