using backend.Application.DTO.Product.SizeDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Requests.Queries
{
    public class GetSizeById : IRequest<SizeResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}