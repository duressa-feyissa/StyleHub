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
    public class CreateProductHandler
        : IRequestHandler<CreateProductRequest, BaseResponse<ProductResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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

            var product = _mapper.Map<Domain.Entities.Product.Product>(request?.Product);
            var user = await _unitOfWork.UserRepository.GetById(request?.UserId ?? "");
            product.User = user;

            if (request?.Product.BrandId != null)
            {
                var brand = await _unitOfWork.BrandRepository.GetById(request.Product.BrandId);
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
                var categories = await _unitOfWork.CategoryRepository.GetByIds(
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

            if (request?.Product.ColorIds.Count > 0)
            {
                var colors = await _unitOfWork.ColorRepository.GetByIds(request.Product.ColorIds);

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
                var sizes = await _unitOfWork.SizeRepository.GetByIds(request.Product.SizeIds);

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
                var materialIds = await _unitOfWork.MaterialRepository.GetByIds(
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

            product = await _unitOfWork.ProductRepository.Add(product);

            return new BaseResponse<ProductResponseDTO>
            {
                Message = "Product Created Successfully",
                Success = true,
                Data = _mapper.Map<ProductResponseDTO>(product)
            };
        }
    }
}
