using Application.DTO.Product.SizeDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Size.Requests.Commands
{
    public class DeleteSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}