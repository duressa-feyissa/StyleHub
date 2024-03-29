using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Location.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Location.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Location.Handlers.Queries
{
    public class GetLocationByIdHandler : IRequestHandler<GetLocationById, LocationResponseDTO>
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public GetLocationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LocationResponseDTO> Handle(
            GetLocationById request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Location = await _unitOfWork.LocationRepository.GetById(request.Id);
            if (Location == null)
            {
                throw new NotFoundException("Location with that {request.Id} does not exist");
            }
            var LocationResponse = _mapper.Map<LocationResponseDTO>(Location);
            return LocationResponse;
        }
    }
}
