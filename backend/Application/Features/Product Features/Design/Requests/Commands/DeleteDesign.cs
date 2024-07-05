using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Requests.Commands
{
    public class DeleteDesignRequest : IRequest<BaseResponse<DesignResponseDTO>>
    {
        public string Id { get; set; } = "";
    }
}