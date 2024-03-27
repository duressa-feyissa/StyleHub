using Application.Contracts;
using Application.DTO.SizeDTO.DTO;
using Application.DTO.SizeDTO.Validations;
using Application.Exceptions;
using Application.Features.Size.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Size.Handlers
{

    public class UpdateSizeHandler : IRequestHandler<UpdateSizeRequest, BaseResponse<SizeResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<SizeResponseDTO>> Handle(UpdateSizeRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseSizeValidation(_unitOfWork.SizeRepository);
            var validationResult = await validator.ValidateAsync(request.Size!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var existingSize = await _unitOfWork.SizeRepository.GetById(request.Id);

            if (existingSize == null)
            {
                throw new NotFoundException("Size Not Found");
            }

            var Size = _mapper.Map(request.Size, existingSize);
            await _unitOfWork.SizeRepository.Update(Size);
            return new BaseResponse<SizeResponseDTO>
            {
                Message = "Size Updated Successfully",
                Success = true,
                Data = _mapper.Map<SizeResponseDTO>(Size)
            };
        }
    }
}