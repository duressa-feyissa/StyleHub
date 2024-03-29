using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Location.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Location.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Location.Handlers.Commands
{
    public class UpdateLocationHandler
        : IRequestHandler<UpdateLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<LocationResponseDTO>> Handle(
            UpdateLocationRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingLocation = await _unitOfWork.LocationRepository.GetById(request.Id);

            if (existingLocation == null)
                throw new NotFoundException("Location Not Found");

            if (request?.Location?.Name != null)
            {
                var existingLocationName = await _unitOfWork.LocationRepository.GetByName(
                    request?.Location?.Name ?? ""
                );
                if (existingLocationName != null)
                    throw new BadRequestException("Location Name Already Exists");
                if (request?.Location?.Name.Length < 3)
                    throw new BadRequestException("Location Name Must Be At Least 3 Characters");
                existingLocation.Name = request?.Location?.Name ?? "";
            }

            if (
                (request?.Location?.Latitude != null && request?.Location?.Longitude == null)
                || (request?.Location?.Latitude == null && request?.Location?.Longitude != null)
            )
                throw new BadRequestException("Both Latitude and Longitude Must Be Provided");

            if (request?.Location?.Latitude != null && request?.Location?.Longitude != null)
            {
                var existingLocationCoordinates =
                    await _unitOfWork.LocationRepository.GetByCoordinates(
                        request.Location?.Latitude ?? 0,
                        request.Location?.Longitude ?? 0
                    );
                if (
                    existingLocationCoordinates != null
                    && existingLocationCoordinates.Id != request.Id
                )
                    throw new BadRequestException("Location Coordinates Already Exists");
                existingLocation.Latitude = request.Location?.Latitude ?? 0;
                existingLocation.Longitude = request.Location?.Longitude ?? 0;
            }

            existingLocation.UpdatedAt = DateTime.Now;
            await _unitOfWork.LocationRepository.Update(existingLocation);
            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Updated Successfully",
                Success = true,
                Data = _mapper.Map<LocationResponseDTO>(existingLocation)
            };
        }
    }
}
