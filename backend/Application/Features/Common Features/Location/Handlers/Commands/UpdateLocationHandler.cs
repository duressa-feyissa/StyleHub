using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Location.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Handlers.Commands
{
    public class UpdateLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        public async Task<BaseResponse<LocationResponseDTO>> Handle(
            UpdateLocationRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingLocation = await unitOfWork.LocationRepository.GetById(request.Id);

            if (existingLocation == null)
                throw new NotFoundException("Location Not Found");

            if (request?.Location?.Name != null)
            {
                var existingLocationName = await unitOfWork.LocationRepository.GetByName(
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
                    await unitOfWork.LocationRepository.GetByCoordinates(
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
            await unitOfWork.LocationRepository.Update(existingLocation);
            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Updated Successfully",
                Success = true,
                Data = mapper.Map<LocationResponseDTO>(existingLocation)
            };
        }
    }
}
