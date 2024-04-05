using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.MaterialDTO.DTO;
using Application.DTO.Product.MaterialDTO.Validations;
using Application.Exceptions;
using Application.Features.Product_Features.Material.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Material.Handlers
{

    public class CreateMaterialHandler : IRequestHandler<CreateMaterialRequest, BaseResponse<MaterialResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<MaterialResponseDTO>> Handle(CreateMaterialRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseMaterialValidation(_unitOfWork.MaterialRepository);
            var validationResult = await validator.ValidateAsync(request.Material!);
            if (!validationResult.IsValid)
            {
                 throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Material = _mapper.Map<Domain.Entities.Product.Material>(request.Material);
            await _unitOfWork.MaterialRepository.Add(Material);

            return new BaseResponse<MaterialResponseDTO>
            {
                Message = "Material Created Successfully",
                Success = true,
                Data = _mapper.Map<MaterialResponseDTO>(Material)
            };


        }
    }

}