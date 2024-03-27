using Application.Contracts;
using Application.DTO.ProductDTO.DTO;
using Application.DTO.ProductDTO.Validations;
using Application.Exceptions;
using Application.Features.Product.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product.Handlers
{

	public class CreateProductHandler : IRequestHandler<CreateProductRequest, BaseResponse<ProductResponseDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;

		}


		public async Task<BaseResponse<ProductResponseDTO>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
		{
			var validator = new BaseProductValidation();
			var validationResult = await validator.ValidateAsync(request.Product!);
			if (!validationResult.IsValid)
			{
				throw new BadRequestException("Invalid Product Data");
			}

			var brand = await _unitOfWork.BrandRepository.GetById(request.Product.BrandId);
			if (brand == null)
			{
				throw new NotFoundException("Brand Not Found");
			}
			
			var location = await _unitOfWork.LocationRepository.GetById(request.Product.LocationId);
			if (location == null)
			{
				throw new NotFoundException("Location Not Found");
			}

			var sizes = await _unitOfWork.SizeRepository.GetByIds(request.Product.SizeIds);
			if (sizes == null || sizes.Count != request.Product.SizeIds.Count)
			{
				throw new NotFoundException("Size Not Found");
			}

			var colors = await _unitOfWork.ColorRepository.GetByIds(request.Product.ColorIds);
			if (colors == null || colors.Count != request.Product.ColorIds.Count)
			{
				throw new NotFoundException("Color Not Found");
			}

			var materials = await _unitOfWork.MaterialRepository.GetByIds(request.Product.MaterialIds);
			if (materials == null || materials.Count != request.Product.MaterialIds.Count)
			{
				throw new NotFoundException("Material Not Found");
			}

			var product = _mapper.Map<Domain.Entities.Product>(request.Product);
			product.Brand = brand;
			product.Location = location;
			await _unitOfWork.ProductRepository.Add(product);

			for (int i = 0; i < colors.Count; i++)
			{
				var productColor = new Domain.Entities.ProductColor
				{
					ProductId = product.Id,
					Color = colors[i]
				};
				await _unitOfWork.ProductColorRepository.Add(productColor);
			}

			for (int i = 0; i < sizes.Count; i++)
			{
				var productSize = new Domain.Entities.ProductSize
				{
					ProductId = product.Id,
					Size = sizes[i]
				};
				await _unitOfWork.ProductSizeRepository.Add(productSize);
			}

			for (int i = 0; i < materials.Count; i++)
			{
				var productMaterial = new Domain.Entities.ProductMaterial
				{
					ProductId = product.Id,
					Material = materials[i]
				};
				await _unitOfWork.ProductMaterialRepository.Add(productMaterial);
			}

			for (int i = 0; i < request.Product.BinaryImages.Count; i++)
			{
				var productImage = new Domain.Entities.ProductImage
				{
					ImageUrl = request.Product.BinaryImages[i],
					ProductId = product.Id
				};
				await _unitOfWork.ProductImageRepository.Add(productImage);
			}

			return new BaseResponse<ProductResponseDTO>
			{
				Message = "Product Created Successfully",
				Success = true,
				Data = _mapper.Map<ProductResponseDTO>(product)
			};
		}
	}

}