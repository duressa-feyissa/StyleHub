using Application.Contracts;
using Application.DTO.LocationDTO.DTO;
using Application.DTO.LocationDTO.Validations;
using Application.Exceptions;
using Application.Features.Location.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Location.Handlers
{

    public class CreateLocationHandler : IRequestHandler<CreateLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<BaseResponse<LocationResponseDTO>> Handle(CreateLocationRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseLocationValidation(_unitOfWork.LocationRepository);
            var validationResult = await validator.ValidateAsync(request.Location!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Location = _mapper.Map<Domain.Entities.Location>(request.Location);
            await _unitOfWork.LocationRepository.Add(Location);

            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Created Successfully",
                Success = true,
                Data = _mapper.Map<LocationResponseDTO>(Location)
            };
        }
    }

}