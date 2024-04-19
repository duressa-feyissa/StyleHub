using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.DTO.Product.MaterialDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Material.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Material.Handlers.Commands
{

    public class CreateMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateMaterialRequest, BaseResponse<MaterialResponseDTO>>
    {
        public async Task<BaseResponse<MaterialResponseDTO>> Handle(CreateMaterialRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseMaterialValidation(unitOfWork.MaterialRepository);
            var validationResult = await validator.ValidateAsync(request.Material!);
            if (!validationResult.IsValid)
            {
                 throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Material = mapper.Map<Domain.Entities.Product.Material>(request.Material);
            await unitOfWork.MaterialRepository.Add(Material);

            return new BaseResponse<MaterialResponseDTO>
            {
                Message = "Material Created Successfully",
                Success = true,
                Data = mapper.Map<MaterialResponseDTO>(Material)
            };


        }
    }

}