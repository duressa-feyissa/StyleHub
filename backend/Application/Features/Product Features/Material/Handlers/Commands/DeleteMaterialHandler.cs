using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.MaterialDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Material.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Material.Handlers.Commands
{

    public class DeleteMaterialHandler : IRequestHandler<DeleteMaterialRequest, BaseResponse<MaterialResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<MaterialResponseDTO>> Handle(DeleteMaterialRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Material Id");
            }

            var Material = await _unitOfWork.MaterialRepository.GetById(request.Id);

            if (Material == null)
            {
                throw new  NotFoundException("Material Not Found");
            }

            await _unitOfWork.MaterialRepository.Delete(Material);

            return new BaseResponse<MaterialResponseDTO>
            {
                Message = "Material Deleted Successfully",
                Success = true,
                Data = _mapper.Map<MaterialResponseDTO>(Material)
            };

        }
    }

}