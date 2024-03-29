using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Location.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Location.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Location.Handlers.Queries
{
    public class GetAllLocationHandler : IRequestHandler<GetAllLocation, List<LocationResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<LocationResponseDTO>> Handle(
            GetAllLocation request,
            CancellationToken cancellationToken
        )
        {
            var Locations = await _unitOfWork.LocationRepository.GetAll();
            if (Locations == null)
            {
                throw new NotFoundException("No Locations found");
            }
            var LocationResponse = _mapper.Map<List<LocationResponseDTO>>(Locations);
            return LocationResponse;
        }
    }
}
