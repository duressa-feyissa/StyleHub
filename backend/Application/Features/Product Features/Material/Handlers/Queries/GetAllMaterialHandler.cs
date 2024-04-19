using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Material.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Material.Handlers.Queries
{
    public class GetAllMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllMaterial, List<MaterialResponseDTO>>
    {
        public async Task<List<MaterialResponseDTO>> Handle(GetAllMaterial request, CancellationToken cancellationToken)
        {
            var Materials = await unitOfWork.MaterialRepository.GetAll();
            if (Materials == null)
            {
                throw new NotFoundException("No Materials found");
            }
            var MaterialResponse = mapper.Map<List<MaterialResponseDTO>>(Materials);
            return MaterialResponse;
        }
    }
}