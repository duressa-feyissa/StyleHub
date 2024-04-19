using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.DTO.Product.SizeDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Size.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Handlers.Commands
{

    public class CreateSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateSizeRequest, BaseResponse<SizeResponseDTO>>
    {
        public async Task<BaseResponse<SizeResponseDTO>> Handle(CreateSizeRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSizeValidation(unitOfWork.SizeRepository);
            var validationResult = await validator.ValidateAsync(request.Size!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Size = mapper.Map<Domain.Entities.Product.Size>(request.Size);
            await unitOfWork.SizeRepository.Add(Size);

            return new BaseResponse<SizeResponseDTO>
            {
                Message = "Size Created Successfully",
                Success = true,
                Data = mapper.Map<SizeResponseDTO>(Size)
            };


        }
    }

}