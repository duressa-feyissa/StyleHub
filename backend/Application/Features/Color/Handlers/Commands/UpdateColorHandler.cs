using Application.Contracts;
using Application.DTO.ColorDTO.DTO;
using Application.DTO.ColorDTO.Validations;
using Application.Exceptions;
using Application.Features.Color.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Application.Features.Color.Handlers
{

    public class UpdateColorHandler : IRequestHandler<UpdateColorRequest, BaseResponse<ColorResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<ColorResponseDTO>> Handle(UpdateColorRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseColorValidation(
                _unitOfWork.ColorRepository
            );
            var validationResult = await validator.ValidateAsync(request.Color!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var existingColor = await _unitOfWork.ColorRepository.GetById(request.Id);

            if (existingColor == null)
            {
                throw new NotFoundException("Color Not Found");
            }

            existingColor.UpdatedAt = DateTime.Now;

            var Color = _mapper.Map(request.Color, existingColor);
            await _unitOfWork.ColorRepository.Update(Color);
            return new BaseResponse<ColorResponseDTO>
            {
                Message = "Color Updated Successfully",
                Success = true,
                Data = _mapper.Map<ColorResponseDTO>(Color)
            };
        }
    }
}