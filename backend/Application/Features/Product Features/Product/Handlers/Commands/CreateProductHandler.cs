using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ProductDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Product.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Product.Requests.Handlers.Commands
{
    public class CreateProductHandler
        : IRequestHandler<CreateProductRequest, BaseResponse<ProductResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IImageUploadRepository _imageUploadRepository;

        public CreateProductHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageUploadRepository imageUploadRepository
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageUploadRepository = imageUploadRepository;
        }

        public async Task<BaseResponse<ProductResponseDTO>> Handle(
            CreateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            IReadOnlyList<Domain.Entities.Product.Color>? colors = null;
            IReadOnlyList<Domain.Entities.Product.Size>? sizes = null;
            IReadOnlyList<Domain.Entities.Product.Material>? materials = null;
            Domain.Entities.Product.Brand? brand = null;

            var validator = new CreateProductValidation();
            var validationResult = await validator.ValidateAsync(request.Product!);
            if (!validationResult.IsValid)
                throw new BadRequestException("Invalid Product Data");

            var location = await _unitOfWork.LocationRepository.GetById(request.Product.LocationId);

            var categories = await _unitOfWork.CategoryRepository.GetByIds(
                request.Product.CategoryIds
            );
            if (categories == null || categories.Count != request.Product.CategoryIds.Count)
                throw new NotFoundException("category Not Found");

            if (location == null)
                throw new NotFoundException("Location Not Found");

            if (request?.Product?.ColorIds != null || request?.Product?.ColorIds?.Count > 0)
            {
                colors = await _unitOfWork.ColorRepository.GetByIds(request.Product.ColorIds);
                if (colors == null || colors?.Count != request.Product.ColorIds.Count)
                    throw new NotFoundException("Color Not Found");
            }

            if (request?.Product?.SizeIds != null || request?.Product?.SizeIds?.Count > 0)
            {
                sizes = await _unitOfWork.SizeRepository.GetByIds(request.Product.SizeIds);
                if (sizes == null || sizes?.Count != request.Product.SizeIds.Count)
                    throw new NotFoundException("Size Not Found");
            }

            if (request?.Product?.MaterialIds != null || request?.Product?.MaterialIds?.Count > 0)
            {
                materials = await _unitOfWork.MaterialRepository.GetByIds(
                    request.Product.MaterialIds
                );
                if (materials == null || materials?.Count != request.Product.MaterialIds.Count)
                    throw new NotFoundException("Material Not Found");
            }

            if (request?.Product?.BrandId == null)
            {
                brand = await _unitOfWork.BrandRepository.GetById(request?.Product?.BrandId!);
                if (brand == null)
                    throw new NotFoundException("Brand Not Found");
            }

            var product = _mapper.Map<Domain.Entities.Product.Product>(request?.Product);
            if (brand != null)
                product.Brand = brand;
            product.Location = location;
            await _unitOfWork.ProductRepository.Add(product);

            if (sizes != null && sizes?.Count > 0)
                for (int i = 0; i < sizes?.Count; i++)
                {
                    var productSize = new Domain.Entities.Product.ProductSize
                    {
                        ProductId = product.Id,
                        Size = sizes[i]
                    };
                    await _unitOfWork.ProductSizeRepository.Add(productSize);
                }

            if (colors != null && colors?.Count > 0)
                for (int i = 0; i < colors?.Count; i++)
                {
                    var productColor = new Domain.Entities.Product.ProductColor
                    {
                        ProductId = product.Id,
                        Color = colors[i]
                    };
                    await _unitOfWork.ProductColorRepository.Add(productColor);
                }

            if (materials != null && materials?.Count > 0)
                for (int i = 0; i < materials?.Count; i++)
                {
                    var productMaterial = new Domain.Entities.Product.ProductMaterial
                    {
                        ProductId = product.Id,
                        Material = materials[i]
                    };
                    await _unitOfWork.ProductMaterialRepository.Add(productMaterial);
                }

            for (int i = 0; i < request?.Product.BinaryImages.Count; i++)
            {
                var productImage = new Domain.Entities.Product.ProductImage
                {
                    ImageUrl = request.Product.BinaryImages[i],
                    ProductId = product.Id
                };
                productImage.Id = await _imageUploadRepository.Upload(
                    productImage.ImageUrl,
                    productImage.Id
                );
                await _unitOfWork.ProductImageRepository.Add(productImage);
            }

            for (int i = 0; i < categories.Count; i++)
            {
                var productCategory = new Domain.Entities.Product.ProductCategory
                {
                    ProductId = product.Id,
                    Category = categories[i]
                };
                await _unitOfWork.ProductCategoryRepository.Add(productCategory);
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
