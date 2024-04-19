using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Requests.Commands
{
    public class UpdateColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateColorDTO? Color { get; set; }
    }
}
