using Application.DTO.SizeDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Size.Requests.Commands
{
    public class UpdateSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public string Id { get; set; } = "";
        public BaseSizeDTO? Size { get; set; }
    }
}