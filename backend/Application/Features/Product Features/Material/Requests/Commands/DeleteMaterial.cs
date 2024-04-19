using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Material.Requests.Commands
{
    public class DeleteMaterialRequest : IRequest<BaseResponse<MaterialResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}