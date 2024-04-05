using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Location.DTO;
using Application.DTO.Common.Location.Validations;
using Application.Exceptions;
using Application.Features.Common_Features.Location.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Location.Handlers.Commands
{
    public class CreateLocationHandler
        : IRequestHandler<CreateLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<LocationResponseDTO>> Handle(
            CreateLocationRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateLocationValidation(_unitOfWork.LocationRepository);
            var validationResult = await validator.ValidateAsync(request.Location!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var _location = _mapper.Map<Domain.Entities.Common.Location>(request.Location);
            await _unitOfWork.LocationRepository.Add(_location);

            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Created Successfully",
                Success = true,
                Data = _mapper.Map<LocationResponseDTO>(_location)
            };
        }
    }
}
