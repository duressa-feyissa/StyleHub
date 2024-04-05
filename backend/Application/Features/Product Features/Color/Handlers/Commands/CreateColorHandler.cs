using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ColorDTO.DTO;
using Application.DTO.Product.ColorDTO.Validations;
using Application.Exceptions;
using Application.Features.Product_Features.Color.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Color.Handlers
{
    public class CreateColorHandler
        : IRequestHandler<CreateColorRequest, BaseResponse<ColorResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ColorResponseDTO>> Handle(
            CreateColorRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateColorValidation(_unitOfWork.ColorRepository);

            var validationResult = await validator.ValidateAsync(request.Color!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Color = _mapper.Map<Domain.Entities.Product.Color>(request.Color);
            await _unitOfWork.ColorRepository.Add(Color);

            return new BaseResponse<ColorResponseDTO>
            {
                Message = "Color Created Successfully",
                Success = true,
                Data = _mapper.Map<ColorResponseDTO>(Color)
            };
        }
    }
}
