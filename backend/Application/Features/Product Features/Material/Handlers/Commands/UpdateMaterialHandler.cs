using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.MaterialDTO.DTO;
using Application.DTO.Product.MaterialDTO.Validations;
using Application.Exceptions;
using Application.Features.Product_Features.Material.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Material.Handlers.Commands
{

    public class UpdateMaterialHandler : IRequestHandler<UpdateMaterialRequest, BaseResponse<MaterialResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<MaterialResponseDTO>> Handle(UpdateMaterialRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseMaterialValidation(_unitOfWork.MaterialRepository);
            var validationResult = await validator.ValidateAsync(request.Material!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var existingMaterial = await _unitOfWork.MaterialRepository.GetById(request.Id);

            if (existingMaterial == null)
            {
                throw new NotFoundException("Material Not Found");
            }

            existingMaterial.UpdatedAt = DateTime.Now;

            var Material = _mapper.Map(request.Material, existingMaterial);
            await _unitOfWork.MaterialRepository.Update(Material);
            return new BaseResponse<MaterialResponseDTO>
            {
                Message = "Material Updated Successfully",
                Success = true,
                Data = _mapper.Map<MaterialResponseDTO>(Material)
            };
        }
    }
}