using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ProductDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Product.Requests.Commands;
using Application.Response;
using AutoMapper;
using Domain.Entities.Product;
using MediatR;

namespace Application.Features.Product_Features.Product.Requests.Handlers.Commands
{
    public class UpdateProductHandler
        : IRequestHandler<UpdateProductRequest, BaseResponse<ProductResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ProductResponseDTO>> Handle(
            UpdateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await _unitOfWork.ProductRepository.GetById(request.Id);

            if (request.Product.Title != null)
                product.Title = request.Product.Title;
            if (request.Product.Description != null)
                product.Description = request.Product.Description;
            if (request.Product.Price != null)
                product.Price = request.Product.Price ?? 1;
            if (request.Product.Quantity != null)
                product.Quantity = request.Product.Quantity ?? 1;
            if (request.Product.Condition != null)
                product.Condition = request.Product.Condition;
            if (request.Product.Target != null)
                product.Target = request.Product.Target;
            if (request.Product.IsPublished != null)
                product.IsPublished = request.Product.IsPublished ?? false;
            if (request.Product.IsNegotiable != null)
                product.IsNegotiable = request.Product.IsNegotiable ?? false;
            if (
                request.Product.Latitude != null
                && request.Product.Latitude > -90
                && request.Product.Latitude < 90
            )
                product.Latitude = request.Product.Latitude ?? 0;
            if (
                request.Product.Longitude != null
                && request.Product.Longitude > -180
                && request.Product.Longitude < 180
            )
                product.Longitude = request.Product.Longitude ?? 0;
            if (request.Product.City != null)
                product.City = request.Product.City;
            if (request.Product.BrandId != null)
            {
                var brand = await _unitOfWork.BrandRepository.GetById(request.Product.BrandId);
                if (brand == null)
                    throw new NotFoundException("Brand Not Found");
                product.Brand = brand;
            }

            if (request.Product.CategoryIds != null && request.Product.CategoryIds.Count > 0)
            {
                var categories = await _unitOfWork.CategoryRepository.GetByIds(
                    request.Product.CategoryIds
                );

                if (categories == null || categories.Count != request.Product.CategoryIds.Count)
                    throw new NotFoundException("category Not Found");

                await _unitOfWork.ProductCategoryRepository.DeleteByProductId(product.Id);

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

            if (request.Product.ColorIds != null && request.Product.ColorIds.Count > 0)
            {
                var colors = await _unitOfWork.ColorRepository.GetByIds(request.Product.ColorIds);

                if (colors == null || colors.Count != request.Product.ColorIds.Count)
                    throw new NotFoundException("color Not Found");

                await _unitOfWork.ProductColorRepository.DeleteByProductId(product.Id);

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

            if (request.Product.SizeIds != null && request.Product.SizeIds.Count > 0)
            {
                var sizes = await _unitOfWork.SizeRepository.GetByIds(request.Product.SizeIds);

                if (sizes == null || sizes.Count != request.Product.SizeIds.Count)
                    throw new NotFoundException("sizes Not Found");

                await _unitOfWork.ProductSizeRepository.DeleteByProductId(product.Id);

                for (int i = 0; i < sizes.Count; i++)
                {
                    var productSize = new ProductSize { ProductId = product.Id, Size = sizes[i] };
                    product.ProductSizes.Add(productSize);
                }
            }

            if (request.Product.MaterialIds != null && request.Product.MaterialIds.Count > 0)
            {
                var materialIds = await _unitOfWork.MaterialRepository.GetByIds(
                    request.Product.MaterialIds
                );

                if (materialIds == null || materialIds.Count != request.Product.MaterialIds.Count)
                    throw new NotFoundException("materialIds Not Found");

                await _unitOfWork.ProductMaterialRepository.DeleteByProductId(product.Id);

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

            product = await _unitOfWork.ProductRepository.Update(product);

            return new BaseResponse<ProductResponseDTO>
            {
                Message = "Product Created Successfully",
                Success = true,
                Data = _mapper.Map<ProductResponseDTO>(product)
            };
        }
    }
}
