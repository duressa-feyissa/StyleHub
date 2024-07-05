using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Design.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Handlers.Queries
{
    public class GetAllDesignHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllDesign, List<DesignResponseDTO>>
    {
        public async Task<List<DesignResponseDTO>> Handle(GetAllDesign request, CancellationToken cancellationToken)
        {
            var Designs = await unitOfWork.DesignRepository.GetAll();
            if (Designs == null)
            {
                throw new NotFoundException("No Designs found");
            }
            var DesignResponse = mapper.Map<List<DesignResponseDTO>>(Designs);
            return DesignResponse;
        }
    }
}