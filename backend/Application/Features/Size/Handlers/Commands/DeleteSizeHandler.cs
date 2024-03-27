using Application.Contracts;
using Application.DTO.SizeDTO.DTO;
using Application.Exceptions;
using Application.Features.Size.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Size.Handlers
{

    public class DeleteSizeHandler : IRequestHandler<DeleteSizeRequest, BaseResponse<SizeResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<SizeResponseDTO>> Handle(DeleteSizeRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Size Id");
            }

            var Size = await _unitOfWork.SizeRepository.GetById(request.Id);

            if (Size == null)
            {
                throw new  NotFoundException("Size Not Found");
            }

            await _unitOfWork.SizeRepository.Delete(Size);

            return new BaseResponse<SizeResponseDTO>
            {
                Message = "Size Deleted Successfully",
                Success = true,
                Data = _mapper.Map<SizeResponseDTO>(Size)
            };

        }
    }

}