using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Requests.Commands
{
    public class UpdateSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateSizeDTO? Size { get; set; }
    }
}
