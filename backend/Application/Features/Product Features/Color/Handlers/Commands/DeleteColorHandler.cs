using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Color.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Handlers.Commands
{
    public class DeleteColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteColorRequest, BaseResponse<ColorResponseDTO>>
    {
        public async Task<BaseResponse<ColorResponseDTO>> Handle(
            DeleteColorRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Color Id");
            }

            var Color = await unitOfWork.ColorRepository.GetById(request.Id);

            if (Color == null)
            {
                throw new NotFoundException("Color Not Found");
            }

            await unitOfWork.ColorRepository.Delete(Color);

            return new BaseResponse<ColorResponseDTO>
            {
                Message = "Color Deleted Successfully",
                Success = true,
                Data = mapper.Map<ColorResponseDTO>(Color)
            };
        }
    }
}
