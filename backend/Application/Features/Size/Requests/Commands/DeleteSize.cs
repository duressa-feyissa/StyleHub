using Application.DTO.SizeDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Size.Requests.Commands
{
    public class DeleteSizeRequest : IRequest<BaseResponse<SizeResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}