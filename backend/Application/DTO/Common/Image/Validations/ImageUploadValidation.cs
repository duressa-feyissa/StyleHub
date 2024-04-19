using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Application.DTO.Common.Image.DTO;
using FluentValidation;

namespace backend.Application.DTO.Common.Image.Validations
{
    public class ImageUploadValidation : AbstractValidator<ImageUploadDTO>
    {
        public ImageUploadValidation()
        {
            RuleFor(x => x.Base64Image)
                .NotNull()
                .WithMessage("Base64Image is required")
                .NotEmpty()
                .WithMessage("Base64Image cannot be empty");

           
        }
    }
}
