using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Design.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Handlers.Queries
{
    public class GetDesignByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetDesignById, DesignResponseDTO>
    {
        public async Task<DesignResponseDTO> Handle(GetDesignById request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                throw new BadRequestException("Id is required");
            }

            var design = await unitOfWork.DesignRepository.GetById(request.Id);
            if (design == null)
            {
                throw new NotFoundException("Design with that {request.Id} does not exist");
            }
            var designResponse = mapper.Map<DesignResponseDTO>(design);
            return designResponse;
        }

    }
}