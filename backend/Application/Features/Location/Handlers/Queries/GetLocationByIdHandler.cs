using Application.DTO.LocationDTO.DTO;
using Application.Features.Location.Requests.Queries;
using MediatR;
using AutoMapper;
using Application.Contracts;
using Application.Exceptions;

namespace Application.Features.Location.Handlers.Queries
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

        public async Task<LocationResponseDTO> Handle(GetLocationById request, CancellationToken cancellationToken)
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