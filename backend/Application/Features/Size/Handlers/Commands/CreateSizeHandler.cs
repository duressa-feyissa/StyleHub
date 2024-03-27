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

    public class CreateSizeHandler : IRequestHandler<CreateSizeRequest, BaseResponse<SizeResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<SizeResponseDTO>> Handle(CreateSizeRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseSizeValidation(_unitOfWork.SizeRepository);
            var validationResult = await validator.ValidateAsync(request.Size!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Size = _mapper.Map<Domain.Entities.Size>(request.Size);
            await _unitOfWork.SizeRepository.Add(Size);

            return new BaseResponse<SizeResponseDTO>
            {
                Message = "Size Created Successfully",
                Success = true,
                Data = _mapper.Map<SizeResponseDTO>(Size)
            };


        }
    }

}