using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Size.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Handlers.Commands
{
    public class UpdateSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateSizeRequest, BaseResponse<SizeResponseDTO>>
    {
        public async Task<BaseResponse<SizeResponseDTO>> Handle(
            UpdateSizeRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingSize = await unitOfWork.SizeRepository.GetById(request.Id);

            if (existingSize == null)
            {
                throw new NotFoundException("Size Not Found");
            }

            if (request?.Size?.Name != null)
            {
                var size = await unitOfWork.SizeRepository.GetByName(request.Size.Name);
                if (size != null && size.Id != request.Id)
                    throw new BadRequestException("Size Name Already Exists");
                existingSize.Name = request.Size.Name.Trim().ToLower();
            }

            if (request?.Size?.Abbreviation != null)
            {
                var size = await unitOfWork.SizeRepository.GetByAbbreviation(
                    request.Size.Abbreviation
                );
                if (size != null && size.Id != request.Id)
                    throw new BadRequestException("Size Abbreviation Already Exists");
                existingSize.Abbreviation = request.Size.Abbreviation.Trim().ToLower();
            }

            existingSize.UpdatedAt = DateTime.Now;
            await unitOfWork.SizeRepository.Update(existingSize);
            return new BaseResponse<SizeResponseDTO>
            {
                Message = "Size Updated Successfully",
                Success = true,
                Data = mapper.Map<SizeResponseDTO>(existingSize)
            };
        }
    }
}
