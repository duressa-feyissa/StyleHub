using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.DTO.Product.DesignDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Design.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Design.Handlers.Commands
{

    public class CreateDesignHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateDesignRequest, BaseResponse<DesignResponseDTO>>
    {
        public async Task<BaseResponse<DesignResponseDTO>> Handle(CreateDesignRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseDesignValidation(unitOfWork.DesignRepository);
            var validationResult = await validator.ValidateAsync(request.Design!);
            if (!validationResult.IsValid)
            {
                 throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var design = mapper.Map<Domain.Entities.Product.Design>(request.Design);
            await unitOfWork.DesignRepository.Add(design);

            return new BaseResponse<DesignResponseDTO>
            {
                Message = "Design Created Successfully",
                Success = true,
                Data = mapper.Map<DesignResponseDTO>(design)
            };


        }
    }

}