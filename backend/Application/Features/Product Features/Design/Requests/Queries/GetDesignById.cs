using backend.Application.DTO.Product.DesignDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Requests.Queries
{
    public class GetDesignById : IRequest<DesignResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}