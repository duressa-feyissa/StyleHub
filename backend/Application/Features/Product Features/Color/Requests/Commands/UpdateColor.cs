using Application.DTO.Product.ColorDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Color.Requests.Commands
{
    public class UpdateColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateColorDTO? Color { get; set; }
    }
}
