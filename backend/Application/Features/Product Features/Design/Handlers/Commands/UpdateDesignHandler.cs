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

    public class UpdateDesignHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateDesignRequest, BaseResponse<DesignResponseDTO>>
    {
        public async Task<BaseResponse<DesignResponseDTO>> Handle(UpdateDesignRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseDesignValidation(unitOfWork.DesignRepository);
            var validationResult = await validator.ValidateAsync(request.Design!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var existingDesign = await unitOfWork.DesignRepository.GetById(request.Id);

            if (existingDesign == null)
            {
                throw new NotFoundException("Design Not Found");
            }

            existingDesign.UpdatedAt = DateTime.Now;

            var design = mapper.Map(request.Design, existingDesign);
            await unitOfWork.DesignRepository.Update(design);
            return new BaseResponse<DesignResponseDTO>
            {
                Message = "Design Updated Successfully",
                Success = true,
                Data = mapper.Map<DesignResponseDTO>(design)
            };
        }
    }
}