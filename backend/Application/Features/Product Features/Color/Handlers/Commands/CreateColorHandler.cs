using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.DTO.Product.ColorDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Color.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Handlers.Commands
{
    public class CreateColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateColorRequest, BaseResponse<ColorResponseDTO>>
    {
        public async Task<BaseResponse<ColorResponseDTO>> Handle(
            CreateColorRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateColorValidation(unitOfWork.ColorRepository);

            var validationResult = await validator.ValidateAsync(request.Color!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Color = mapper.Map<Domain.Entities.Product.Color>(request.Color);
            await unitOfWork.ColorRepository.Add(Color);

            return new BaseResponse<ColorResponseDTO>
            {
                Message = "Color Created Successfully",
                Success = true,
                Data = mapper.Map<ColorResponseDTO>(Color)
            };
        }
    }
}
