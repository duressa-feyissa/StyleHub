using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Location.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Handlers.Queries
{
    public class GetLocationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetLocationById, LocationResponseDTO>
    {
        public async Task<LocationResponseDTO> Handle(
            GetLocationById request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Location = await unitOfWork.LocationRepository.GetById(request.Id);
            if (Location == null)
            {
                throw new NotFoundException("Location with that {request.Id} does not exist");
            }
            var LocationResponse = mapper.Map<LocationResponseDTO>(Location);
            return LocationResponse;
        }
    }
}
