using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Color.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Handlers.Commands
{
	public class UpdateColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		: IRequestHandler<UpdateColorRequest, BaseResponse<ColorResponseDTO>>
	{
		public async Task<BaseResponse<ColorResponseDTO>> Handle(
			UpdateColorRequest request,
			CancellationToken cancellationToken
		)
		{
			var existingColor = await unitOfWork.ColorRepository.GetById(request.Id);

			if (existingColor == null)
			{
				throw new NotFoundException("Color Not Found");
			}
			
			if (request?.Color?.Name != null)
			{
				var existingColorName = await unitOfWork.ColorRepository.GetByName(
					request.Color.Name
				);
				if (existingColorName != null && existingColorName.Id != request.Id)
				{
					throw new BadRequestException("Color Name Already Exists");
				}
				if (request.Color.Name.Length == 0)
				{
					throw new BadRequestException("Color Name Cannot Be Empty");
				}
				existingColor.Name = request.Color.Name.Trim().ToLower();
			}
			
			if (request?.Color?.HexCode != null)
			{
				var existingColorHexCode = await unitOfWork.ColorRepository.GetByHexCode(
					request.Color.HexCode
				);
				if (existingColorHexCode != null && existingColorHexCode.Id != request.Id)
				{
					throw new BadRequestException("Color HexCode Already Exists");
				}
				if (request.Color.HexCode.Length == 0)
				{
					throw new BadRequestException("Color HexCode Cannot Be Empty");
				}
				existingColor.HexCode = request.Color.HexCode.Trim().ToLower();
			}

			existingColor.UpdatedAt = DateTime.Now;

			await unitOfWork.ColorRepository.Update(existingColor);
			return new BaseResponse<ColorResponseDTO>
			{
				Message = "Color Updated Successfully",
				Success = true,
				Data = mapper.Map<ColorResponseDTO>(existingColor)
			};
		}
	}
}
