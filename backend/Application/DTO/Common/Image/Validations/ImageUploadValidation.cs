using Application.Contracts.Persistence.Repositories.Common;
using Application.Contracts.Persistence.Repositories.Product;
using Application.DTO.Common.Image.DTO;
using FluentValidation;

namespace Application.DTO.Common.Location.Validations
{
    public class ImageUploadValidation : AbstractValidator<ImageUploadDTO>
    {
        IProductRepository _productRepository;

        public ImageUploadValidation(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Base64Image)
                .NotNull()
                .WithMessage("Base64Image is required")
                .NotEmpty()
                .WithMessage("Base64Image cannot be empty");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .When(x => x.ProductId != null)
                .WithMessage("ProductId cannot be empty when provided.")
                .MustAsync(
                    async (productId, cancellation) =>
                    {
                        if (productId != null)
                            return await _productRepository.GetById(productId) != null;
                        return true;
                    }
                )
                .WithMessage("Specified ProductId does not exist.")
                .When(x => x.ProductId != null);
        }
    }
}
