using Application.DTO.SizeDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Size.Requests.Commands
{
    public class CreateSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public BaseSizeDTO? Size { get; set; }
    }
}