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

    public class UpdateMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateMaterialRequest, BaseResponse<MaterialResponseDTO>>
    {
        public async Task<BaseResponse<MaterialResponseDTO>> Handle(UpdateMaterialRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseMaterialValidation(unitOfWork.MaterialRepository);
            var validationResult = await validator.ValidateAsync(request.Material!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var existingMaterial = await unitOfWork.MaterialRepository.GetById(request.Id);

            if (existingMaterial == null)
            {
                throw new NotFoundException("Material Not Found");
            }

            existingMaterial.UpdatedAt = DateTime.Now;

            var Material = mapper.Map(request.Material, existingMaterial);
            await unitOfWork.MaterialRepository.Update(Material);
            return new BaseResponse<MaterialResponseDTO>
            {
                Message = "Material Updated Successfully",
                Success = true,
                Data = mapper.Map<MaterialResponseDTO>(Material)
            };
        }
    }
}