using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.MaterialDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Material.Requests.Queries;
using AutoMapper;
using MediatR;



namespace Application.Features.Product_Features.Material.Handlers.Queries
{
    public class GetAllMaterialHandler : IRequestHandler<GetAllMaterial, List<MaterialResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MaterialResponseDTO>> Handle(GetAllMaterial request, CancellationToken cancellationToken)
        {
            var Materials = await _unitOfWork.MaterialRepository.GetAll();
            if (Materials == null)
            {
                throw new NotFoundException("No Materials found");
            }
            var MaterialResponse = _mapper.Map<List<MaterialResponseDTO>>(Materials);
            return MaterialResponse;
        }
    }
}