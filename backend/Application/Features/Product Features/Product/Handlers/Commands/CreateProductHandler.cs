using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.DTO.Product.ProductDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Product.Requests.Commands;
using backend.Application.Response;
using backend.Domain.Entities.Common;
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
            if (request?.Product.CategoryIds.Count > 0)
            {
                var categories = await unitOfWork.CategoryRepository.GetByIds(
                    request.Product.CategoryIds
                );

                if (categories == null || categories.Count != request.Product.CategoryIds.Count)
                    throw new NotFoundException("category Not Found");

                foreach (var t in categories)
                {
                    var productCategory = new ProductCategory
                    {
                        ProductId = product.Id,
                        Category = t
                    };
                    product.ProductCategories.Add(productCategory);
                }
            }
            
            if (request?.Product.ImageIds.Count > 0)
            {
                var images = await unitOfWork.ImageRepository.GetByIds(request.Product.ImageIds);

                if (images == null || images.Count != request.Product.ImageIds.Count)
                    throw new NotFoundException("image Not Found");

                foreach (var t in images)
                {
                    var productImage = new ProductImage
                    {
                        ProductId = product.Id,
                        Image = t
                    };
                    product.ProductImages.Add(productImage);
                }
            }

            if (request?.Product.ColorIds.Count > 0)
            {
                var colors = await unitOfWork.ColorRepository.GetByIds(request.Product.ColorIds);

                if (colors == null || colors.Count != request.Product.ColorIds.Count)
                    throw new NotFoundException("color Not Found");

                foreach (var t in colors)
                {
                    var productColor = new ProductColor
                    {
                        ProductId = product.Id,
                        Color = t
                    };
                    product.ProductColors.Add(productColor);
                }
            }

            if (request?.Product.SizeIds.Count > 0)
            {
                var sizes = await unitOfWork.SizeRepository.GetByIds(request.Product.SizeIds);

                if (sizes == null || sizes.Count != request.Product.SizeIds.Count)
                    throw new NotFoundException("sizes Not Found");

                foreach (var t in sizes)
                {
                    var productSize = new ProductSize { ProductId = product.Id, Size = t };
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

                foreach (var t in materialIds)
                {
                    var productMaterial = new ProductMaterial
                    {
                        ProductId = product.Id,
                        Material = t
                    };
                    product.ProductMaterials.Add(productMaterial);
                }
            }
            
            if (request?.Product.BrandIds.Count > 0)
            {
                var brands = await unitOfWork.BrandRepository.GetByIds(request.Product.BrandIds);

                if (brands == null || brands.Count != request.Product.BrandIds.Count)
                    throw new NotFoundException("brand Not Found");

                foreach (var t in brands)
                {
                    var productBrand = new ProductBrand
                    {
                        ProductId = product.Id,
                        Brand = t
                    };
                    product.ProductBrands.Add(productBrand);
                }
            }
            
            if (request?.Product.DesignIds.Count > 0)
            {
                var designs = await unitOfWork.DesignRepository.GetByIds(request.Product.DesignIds);

                if (designs == null || designs.Count != request.Product.DesignIds.Count)
                    throw new NotFoundException("design Not Found");

                foreach (var t in designs)
                {
                    var productDesign = new ProductDesign
                    {
                        ProductId = product.Id,
                        Design = t
                    };
                    product.ProductDesigns.Add(productDesign);
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
