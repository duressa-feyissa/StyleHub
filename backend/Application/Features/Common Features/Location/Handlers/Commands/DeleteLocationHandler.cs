using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Location.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Handlers.Commands
{

    public class DeleteLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        public async Task<BaseResponse<LocationResponseDTO>> Handle(DeleteLocationRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Location Id");
            }

            var Location = await unitOfWork.LocationRepository.GetById(request.Id);

            if (Location == null)
            {
                throw new  NotFoundException("Location Not Found");
            }

            await unitOfWork.LocationRepository.Delete(Location);

            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Deleted Successfully",
                Success = true,
                Data = mapper.Map<LocationResponseDTO>(Location)
            };

        }
    }

}