using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Material.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Material.Handlers.Commands
{

    public class DeleteMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteMaterialRequest, BaseResponse<MaterialResponseDTO>>
    {
        public async Task<BaseResponse<MaterialResponseDTO>> Handle(DeleteMaterialRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Material Id");
            }

            var Material = await unitOfWork.MaterialRepository.GetById(request.Id);

            if (Material == null)
            {
                throw new  NotFoundException("Material Not Found");
            }

            await unitOfWork.MaterialRepository.Delete(Material);

            return new BaseResponse<MaterialResponseDTO>
            {
                Message = "Material Deleted Successfully",
                Success = true,
                Data = mapper.Map<MaterialResponseDTO>(Material)
            };

        }
    }

}