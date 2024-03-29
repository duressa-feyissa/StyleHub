using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ColorDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Color.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Color.Handlers.Commands
{
    public class DeleteColorHandler
        : IRequestHandler<DeleteColorRequest, BaseResponse<ColorResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ColorResponseDTO>> Handle(
            DeleteColorRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Color Id");
            }

            var Color = await _unitOfWork.ColorRepository.GetById(request.Id);

            if (Color == null)
            {
                throw new NotFoundException("Color Not Found");
            }

            await _unitOfWork.ColorRepository.Delete(Color);

            return new BaseResponse<ColorResponseDTO>
            {
                Message = "Color Deleted Successfully",
                Success = true,
                Data = _mapper.Map<ColorResponseDTO>(Color)
            };
        }
    }
}
