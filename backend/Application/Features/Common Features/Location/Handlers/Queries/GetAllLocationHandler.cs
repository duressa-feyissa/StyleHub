using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Location.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Handlers.Queries
{
    public class GetAllLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllLocation, List<LocationResponseDTO>>
    {
        public async Task<List<LocationResponseDTO>> Handle(
            GetAllLocation request,
            CancellationToken cancellationToken
        )
        {
            var Locations = await unitOfWork.LocationRepository.GetAll();
            if (Locations == null)
            {
                throw new NotFoundException("No Locations found");
            }
            var LocationResponse = mapper.Map<List<LocationResponseDTO>>(Locations);
            return LocationResponse;
        }
    }
}
