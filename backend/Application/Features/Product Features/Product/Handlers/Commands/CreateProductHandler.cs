using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.DTO.Product.ProductDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Product.Requests.Commands;
using backend.Application.Response;
using backend.Domain.Entities.Product;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Commands
{
    public class CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateProductRequest, BaseResponse<ProductResponseDTO>>
    {
        public async Task<BaseResponse<ProductResponseDTO>> Handle(
            CreateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateProductValidation();
            var validationResult = await validator.ValidateAsync(request.Product!);
            if (!validationResult.IsValid)
                throw new BadRequestException(
                    validationResult.Errors.FirstOrDefault()?.ErrorMessage!
                );

            var product = mapper.Map<Domain.Entities.Product.Product>(request?.Product);
            var user = await unitOfWork.UserRepository.GetById(request?.UserId ?? "");
            product.User = user;

            if (request?.Product.BrandId != null)
            {
                var brand = await unitOfWork.BrandRepository.GetById(request.Product.BrandId);
                if (brand == null)
                    throw new NotFoundException("Brand Not Found");
                product.Brand = brand;
            }
            else
            {
                throw new BadRequestException("Brand Id is Required");
            }
            
           

            if (request?.Product.CategoryIds.Count > 0)
            {
                var categories = await unitOfWork.CategoryRepository.GetByIds(
                    request.Product.CategoryIds
                );

                if (categories == null || categories.Count != request.Product.CategoryIds.Count)
                    throw new NotFoundException("category Not Found");

                for (int i = 0; i < categories.Count; i++)
                {
                    var productCategory = new ProductCategory
                    {
                        ProductId = product.Id,
                        Category = categories[i]
                    };
                    product.ProductCategories.Add(productCategory);
                }
            }
            
            if (request?.Product.ImageIds.Count > 0)
            {
                var images = await unitOfWork.ImageRepository.GetByIds(request.Product.ImageIds);

                if (images == null || images.Count != request.Product.ImageIds.Count)
                    throw new NotFoundException("image Not Found");

                for (int i = 0; i < images.Count; i++)
                {
                    var productImage = new ProductImage
                    {
                        ProductId = product.Id,
                        Image = images[i]
                    };
                    product.ProductImages.Add(productImage);
                }
            }

            if (request?.Product.ColorIds.Count > 0)
            {
                var colors = await unitOfWork.ColorRepository.GetByIds(request.Product.ColorIds);

                if (colors == null || colors.Count != request.Product.ColorIds.Count)
                    throw new NotFoundException("color Not Found");

                for (int i = 0; i < colors.Count; i++)
                {
                    var productColor = new ProductColor
                    {
                        ProductId = product.Id,
                        Color = colors[i]
                    };
                    product.ProductColors.Add(productColor);
                }
            }

            if (request?.Product.SizeIds.Count > 0)
            {
                var sizes = await unitOfWork.SizeRepository.GetByIds(request.Product.SizeIds);

                if (sizes == null || sizes.Count != request.Product.SizeIds.Count)
                    throw new NotFoundException("sizes Not Found");

                for (int i = 0; i < sizes.Count; i++)
                {
                    var productSize = new ProductSize { ProductId = product.Id, Size = sizes[i] };
                    product.ProductSizes.Add(productSize);
                }
            }

            if (request?.Product.MaterialIds.Count > 0)
            {
                var materialIds = await unitOfWork.MaterialRepository.GetByIds(
                    request.Product.MaterialIds
                );

                if (materialIds == null || materialIds.Count != request.Product.MaterialIds.Count)
                    throw new NotFoundException("materialIds Not Found");

                for (int i = 0; i < materialIds.Count; i++)
                {
                    var productMaterial = new ProductMaterial
                    {
                        ProductId = product.Id,
                        Material = materialIds[i]
                    };
                    product.ProductMaterials.Add(productMaterial);
                }
            }

            product = await unitOfWork.ProductRepository.Add(product);

            return new BaseResponse<ProductResponseDTO>
            {
                Message = "Product Created Successfully",
                Success = true,
                Data = mapper.Map<ProductResponseDTO>(product)
            };
        }
    }
}
