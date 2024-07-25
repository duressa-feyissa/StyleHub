using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Product.Requests.Commands;
using backend.Application.Response;
using backend.Domain.Entities.Product;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Commands
{
    public class UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateProductRequest, BaseResponse<ProductResponseDTO>>
    {
        public async Task<BaseResponse<ProductResponseDTO>> Handle(
            UpdateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.ProductRepository.GetById(request.Id);

            if (request.Product.Title != null)
                product.Title = request.Product.Title;
            if (request.Product.Description != null)
                product.Description = request.Product.Description;
            if (request.Product.Price != null)
                product.Price = request.Product.Price ?? 1;
            if (request.Product.InStock != null)
                product.InStock = request.Product.InStock ?? false;
            if (request.Product.Condition != null)
                product.Condition = request.Product.Condition;
            if (request.Product.Status != null)
                product.Status = request.Product.Status;
            if (request.Product.IsNegotiable != null)
                product.IsNegotiable = request.Product.IsNegotiable ?? false;
            if (request.Product.VideoUrl != null)
                product.VideoUrl = request.Product.VideoUrl;
            if (request.Product.CategoryIds != null && request.Product.CategoryIds.Count > 0)
            {
                var categories = await unitOfWork.CategoryRepository.GetByIds(
                    request.Product.CategoryIds
                );

                if (categories == null || categories.Count != request.Product.CategoryIds.Count)
                    throw new NotFoundException("category Not Found");

                await unitOfWork.ProductCategoryRepository.DeleteByProductId(product.Id);

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

            if (request.Product.ColorIds != null && request.Product.ColorIds.Count > 0)
            {
                var colors = await unitOfWork.ColorRepository.GetByIds(request.Product.ColorIds);

                if (colors == null || colors.Count != request.Product.ColorIds.Count)
                    throw new NotFoundException("color Not Found");

                await unitOfWork.ProductColorRepository.DeleteByProductId(product.Id);

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

            if (request.Product.SizeIds != null && request.Product.SizeIds.Count > 0)
            {
                var sizes = await unitOfWork.SizeRepository.GetByIds(request.Product.SizeIds);

                if (sizes == null || sizes.Count != request.Product.SizeIds.Count)
                    throw new NotFoundException("sizes Not Found");

                await unitOfWork.ProductSizeRepository.DeleteByProductId(product.Id);

                foreach (var t in sizes)
                {
                    var productSize = new ProductSize { ProductId = product.Id, Size = t };
                    product.ProductSizes.Add(productSize);
                }
            }

            if (request.Product.MaterialIds != null && request.Product.MaterialIds.Count > 0)
            {
                var materialIds = await unitOfWork.MaterialRepository.GetByIds(
                    request.Product.MaterialIds
                );

                if (materialIds == null || materialIds.Count != request.Product.MaterialIds.Count)
                    throw new NotFoundException("materialIds Not Found");

                await unitOfWork.ProductMaterialRepository.DeleteByProductId(product.Id);

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
            
            if (request.Product.BrandIds != null && request.Product.BrandIds.Count > 0)
            {
                var brands = await unitOfWork.BrandRepository.GetByIds(request.Product.BrandIds);

                if (brands == null || brands.Count != request.Product.BrandIds.Count)
                    throw new NotFoundException("brands Not Found");

                await unitOfWork.ProductBrandRepository.DeleteByProductId(product.Id);

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
            
            if (request.Product.DesignIds != null && request.Product.DesignIds.Count > 0)
            {
                var designs = await unitOfWork.DesignRepository.GetByIds(request.Product.DesignIds);

                if (designs == null || designs.Count != request.Product.DesignIds.Count)
                    throw new NotFoundException("designs Not Found");

                await unitOfWork.ProductDesignRepository.DeleteByProductId(product.Id);

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

            product = await unitOfWork.ProductRepository.Update(product);

            return new BaseResponse<ProductResponseDTO>
            {
                Message = "Product Created Successfully",
                Success = true,
                Data = mapper.Map<ProductResponseDTO>(product)
            };
        }
    }
}
