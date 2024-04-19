using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Location.DTO;
using backend.Application.DTO.Common.Location.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Location.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Location.Handlers.Commands
{
    public class CreateLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        public async Task<BaseResponse<LocationResponseDTO>> Handle(
            CreateLocationRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateLocationValidation(unitOfWork.LocationRepository);
            var validationResult = await validator.ValidateAsync(request.Location!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var _location = mapper.Map<Domain.Entities.Common.Location>(request.Location);
            await unitOfWork.LocationRepository.Add(_location);

            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Created Successfully",
                Success = true,
                Data = mapper.Map<LocationResponseDTO>(_location)
            };
        }
    }
}
