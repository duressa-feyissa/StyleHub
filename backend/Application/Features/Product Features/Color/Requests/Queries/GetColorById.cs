using backend.Application.DTO.Product.ColorDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Requests.Queries
{
    public class GetColorById : IRequest<ColorResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}