using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Size.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Handlers.Commands
{

    public class DeleteSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteSizeRequest, BaseResponse<SizeResponseDTO>>
    {
        public async Task<BaseResponse<SizeResponseDTO>> Handle(DeleteSizeRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Size Id");
            }

            var Size = await unitOfWork.SizeRepository.GetById(request.Id);

            if (Size == null)
            {
                throw new  NotFoundException("Size Not Found");
            }

            await unitOfWork.SizeRepository.Delete(Size);

            return new BaseResponse<SizeResponseDTO>
            {
                Message = "Size Deleted Successfully",
                Success = true,
                Data = mapper.Map<SizeResponseDTO>(Size)
            };

        }
    }

}