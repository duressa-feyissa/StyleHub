using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Image.DTO;
using Application.DTO.Common.Location.Validations;
using Application.Exceptions;
using Application.Features.Common_Features.Image.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;
using IImageRepository = Application.Contracts.Infrastructure.Repositories.IImageRepository;

namespace Application.Features.Common_Features.Image.Handlers.Commads
{
	public class UploadImageHandler
		: IRequestHandler<UploadImageRequest, BaseResponse<ImageResponseDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IImageRepository _imageRepository;

		public UploadImageHandler(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			IImageRepository imageRepository
		)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_imageRepository = imageRepository;
		}

		public async Task<BaseResponse<ImageResponseDTO>> Handle(
			UploadImageRequest request,
			CancellationToken cancellationToken
		)
		{
			var validator = new ImageUploadValidation(_unitOfWork.ProductRepository);
			var validationResult = await validator.ValidateAsync(request.Image!);
			if (!validationResult.IsValid)
			{
				throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
			}

			var user = await _unitOfWork.UserRepository.GetById(request.UserId);
			var product = await _unitOfWork.ProductRepository.GetById(request.Image.ProductId ?? "");

			var _image = new Domain.Entities.Common.Image
			{
				ImageUrl = request.Image.Base64Image,
				Product = product,
				User = user
			};
			_image.ImageUrl = await _imageRepository.Upload(request.Image.Base64Image, _image.Id);
			await _unitOfWork.ImageRepository.Add(_image);

			return new BaseResponse<ImageResponseDTO>
			{
				Message = "Image Uploaded Successfully",
				Success = true,
				Data = _mapper.Map<ImageResponseDTO>(_image)
			};
		}
	}
}
