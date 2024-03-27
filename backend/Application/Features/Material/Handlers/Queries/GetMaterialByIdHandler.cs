using Application.DTO.MaterialDTO.DTO;
using Application.Features.Material.Requests.Queries;
using MediatR;
using AutoMapper;
using Application.Contracts;
using Application.Exceptions;

namespace Application.Features.Material.Handlers.Queries
{
    public class GetMaterialByIdHandler : IRequestHandler<GetMaterialById, MaterialResponseDTO>
    {

        private readonly IMapper _mapper;


        private readonly IUnitOfWork _unitOfWork;

        public GetMaterialByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MaterialResponseDTO> Handle(GetMaterialById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Material = await _unitOfWork.MaterialRepository.GetById(request.Id);
            if (Material == null)
            {
                throw new NotFoundException("Material with that {request.Id} does not exist");
            }
            var MaterialResponse = _mapper.Map<MaterialResponseDTO>(Material);
            return MaterialResponse;
        }

    }
}