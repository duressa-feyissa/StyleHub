using Application.DTO.Product.MaterialDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Material.Requests.Commands
{
    public class DeleteMaterialRequest : IRequest<BaseResponse<MaterialResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}