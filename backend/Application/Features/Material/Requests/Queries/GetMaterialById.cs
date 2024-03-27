using Application.DTO.MaterialDTO.DTO;
using MediatR;

namespace Application.Features.Material.Requests.Queries
{
    public class GetMaterialById : IRequest<MaterialResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}