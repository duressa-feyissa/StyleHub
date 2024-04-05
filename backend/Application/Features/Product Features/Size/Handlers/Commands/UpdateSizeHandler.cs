using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.SizeDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Size.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Size.Handlers.Commands
{
    public class UpdateSizeHandler
        : IRequestHandler<UpdateSizeRequest, BaseResponse<SizeResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SizeResponseDTO>> Handle(
            UpdateSizeRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingSize = await _unitOfWork.SizeRepository.GetById(request.Id);

            if (existingSize == null)
            {
                throw new NotFoundException("Size Not Found");
            }

            if (request?.Size?.Name != null)
            {
                var size = await _unitOfWork.SizeRepository.GetByName(request.Size.Name);
                if (size != null && size.Id != request.Id)
                    throw new BadRequestException("Size Name Already Exists");
                existingSize.Name = request.Size.Name.Trim().ToLower();
            }

            if (request?.Size?.Abbreviation != null)
            {
                var size = await _unitOfWork.SizeRepository.GetByAbbreviation(
                    request.Size.Abbreviation
                );
                if (size != null && size.Id != request.Id)
                    throw new BadRequestException("Size Abbreviation Already Exists");
                existingSize.Abbreviation = request.Size.Abbreviation.Trim().ToLower();
            }

            existingSize.UpdatedAt = DateTime.Now;
            await _unitOfWork.SizeRepository.Update(existingSize);
            return new BaseResponse<SizeResponseDTO>
            {
                Message = "Size Updated Successfully",
                Success = true,
                Data = _mapper.Map<SizeResponseDTO>(existingSize)
            };
        }
    }
}
