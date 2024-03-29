using Application.DTO.Product.ColorDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Color.Requests.Commands
{
    public class DeleteColorRequest : IRequest<BaseResponse<ColorResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}