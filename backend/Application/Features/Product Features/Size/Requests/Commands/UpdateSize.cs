using Application.DTO.Product.SizeDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Size.Requests.Commands
{
    public class UpdateSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateSizeDTO? Size { get; set; }
    }
}
