using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ColorDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Color.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Color.Handlers.Commands
{
	public class UpdateColorHandler
		: IRequestHandler<UpdateColorRequest, BaseResponse<ColorResponseDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<BaseResponse<ColorResponseDTO>> Handle(
			UpdateColorRequest request,
			CancellationToken cancellationToken
		)
		{
			var existingColor = await _unitOfWork.ColorRepository.GetById(request.Id);

			if (existingColor == null)
			{
				throw new NotFoundException("Color Not Found");
			}
			
			if (request?.Color?.Name != null)
			{
				var existingColorName = await _unitOfWork.ColorRepository.GetByName(
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
				var existingColorHexCode = await _unitOfWork.ColorRepository.GetByHexCode(
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

			await _unitOfWork.ColorRepository.Update(existingColor);
			return new BaseResponse<ColorResponseDTO>
			{
				Message = "Color Updated Successfully",
				Success = true,
				Data = _mapper.Map<ColorResponseDTO>(existingColor)
			};
		}
	}
}
