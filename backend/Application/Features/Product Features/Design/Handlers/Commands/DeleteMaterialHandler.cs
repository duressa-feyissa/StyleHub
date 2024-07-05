using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Design.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Handlers.Commands
{

    public class DeleteDesignHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteDesignRequest, BaseResponse<DesignResponseDTO>>
    {
        public async Task<BaseResponse<DesignResponseDTO>> Handle(DeleteDesignRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Design Id");
            }

            var design = await unitOfWork.DesignRepository.GetById(request.Id);

            if (design == null)
            {
                throw new  NotFoundException("Design Not Found");
            }

            await unitOfWork.DesignRepository.Delete(design);

            return new BaseResponse<DesignResponseDTO>
            {
                Message = "Design Deleted Successfully",
                Success = true,
                Data = mapper.Map<DesignResponseDTO>(design)
            };

        }
    }

}