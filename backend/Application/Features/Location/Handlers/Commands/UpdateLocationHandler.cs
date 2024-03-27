using Application.Contracts;
using Application.DTO.LocationDTO.DTO;
using Application.DTO.LocationDTO.Validations;
using Application.Exceptions;
using Application.Features.Location.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Application.Features.Location.Handlers
{

    public class UpdateLocationHandler : IRequestHandler<UpdateLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<LocationResponseDTO>> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseLocationValidation(_unitOfWork.LocationRepository);
            var validationResult = await validator.ValidateAsync(request.Location!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var existingLocation = await _unitOfWork.LocationRepository.GetById(request.Id);

            if (existingLocation == null)
            {
                throw new NotFoundException("Location Not Found");
            }

            existingLocation.UpdatedAt = DateTime.Now;

            var Location = _mapper.Map(request.Location, existingLocation);
            await _unitOfWork.LocationRepository.Update(Location);
            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Updated Successfully",
                Success = true,
                Data = _mapper.Map<LocationResponseDTO>(Location)
            };
        }
    }
}