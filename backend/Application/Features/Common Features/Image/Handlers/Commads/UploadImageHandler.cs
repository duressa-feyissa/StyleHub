using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.DTO.Common.Image.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Image.Requests.Commands;
using backend.Application.Response;
using MediatR;
using IImageRepository = backend.Application.Contracts.Infrastructure.Repositories.IImageRepository;

namespace backend.Application.Features.Common_Features.Image.Handlers.Commads
{
	public class UploadImageHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper,
		IImageRepository imageRepository)
		: IRequestHandler<UploadImageRequest, BaseResponse<ImageResponseDTO>>
	{
		public async Task<BaseResponse<ImageResponseDTO>> Handle(
			UploadImageRequest request,
			CancellationToken cancellationToken
		)
		{
			var validator = new ImageUploadValidation();
			var validationResult = await validator.ValidateAsync(request.Image!);
			if (!validationResult.IsValid)
			{
				throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
			}

			var user = await unitOfWork.UserRepository.GetById(request.UserId);
			

			var _image = new Domain.Entities.Common.Image
			{
				ImageUrl = request.Image.Base64Image,
				User = user
			};
			_image.ImageUrl = await imageRepository.Upload(request.Image.Base64Image, _image.Id);
			await unitOfWork.ImageRepository.Add(_image);

			return new BaseResponse<ImageResponseDTO>
			{
				Message = "Image Uploaded Successfully",
				Success = true,
				Data = mapper.Map<ImageResponseDTO>(_image)
			};
		}
	}
}
