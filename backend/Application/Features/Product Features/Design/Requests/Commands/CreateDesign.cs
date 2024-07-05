using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Requests.Commands
{
    public class CreateDesignRequest : IRequest<BaseResponse<DesignResponseDTO>>
    {
        public BaseDesignDTO? Design { get; set; }
    }
}