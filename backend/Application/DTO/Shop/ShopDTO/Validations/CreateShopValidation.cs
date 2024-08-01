using backend.Application.Contracts.Persistence.Repositories.Shop;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.Shop.ShopDTO.Validations
{
    public class CreateShopValidation : AbstractValidator<CreateShopDTO>
    {
        public CreateShopValidation(IShopRepository shopRepository)
        {
            List<string> socialMedias = new List<string>
            {
                "facebook", "twitter", "instagram", "linkedin", "youtube", "pinterest", "tiktok", "snapchat", "whatsapp", "telegram"
            };
            
            List<string> categories = new List<string>
            {
                "men's fashion", "women's fashion", "kids fashion", "health & beauty", "sports & outdoors", "other"
            };

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long")
                .MaximumLength(50)
                .WithMessage("Name must be at most 50 characters long");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Description is required")
                .NotEmpty()
                .WithMessage("Description cannot be empty")
                .MinimumLength(3)
                .WithMessage("Description must be at least 3 characters long")
                .MaximumLength(512)
                .WithMessage("Description must be at most 512 characters long");

            RuleFor(x => x.Categories)
                .NotNull()
                .WithMessage("Category is required")
                .ForEach(categoryRule => 
                    categoryRule.Must(category => categories.Contains(category))
                        .WithMessage("Category must be one of the following: " + string.Join(", ", categories))
                );

            RuleFor(x => x.Street)
                .NotNull()
                .WithMessage("Street is required")
                .NotEmpty()
                .WithMessage("Street cannot be empty");

            RuleFor(x => x.SubLocality)
                .NotNull()
                .WithMessage("SubLocality is required")
                .NotEmpty()
                .WithMessage("SubLocality cannot be empty");

            RuleFor(x => x.SubAdministrativeArea)
                .NotNull()
                .WithMessage("SubAdministrativeArea is required")
                .NotEmpty()
                .WithMessage("SubAdministrativeArea cannot be empty");

            RuleFor(x => x.Latitude)
                .NotNull()
                .WithMessage("Latitude is required")
                .GreaterThanOrEqualTo(-90)
                .WithMessage("Latitude must be greater than or equal to -90");

            RuleFor(x => x.Longitude)
                .NotNull()
                .WithMessage("Longitude is required")
                .LessThanOrEqualTo(180)
                .WithMessage("Longitude must be less than or equal to 180");

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .WithMessage("PhoneNumber is required")
                .NotEmpty()
                .WithMessage("PhoneNumber cannot be empty");

            RuleFor(x => x.Logo)
                .NotNull()
                .WithMessage("Logo is required")
                .NotEmpty()
                .WithMessage("Logo cannot be empty");

            RuleFor(x => x.Banner)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Banner is required")
                .NotEmpty()
                .WithMessage("Banner cannot be empty");

            RuleFor(x => x.SocialMediaLinks)
                .NotNull()
                .WithMessage("SocialMediaLinks is required")
                .Must(x => x.Keys.All(socialMedias.Contains) &&
                           x.Values.All(s => Uri.IsWellFormedUriString(s, UriKind.Absolute)))
                .WithMessage("SocialMediaLinks must contain valid URLs for recognized social media platforms.");

            RuleFor(x => x.Website)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Website is required")
                .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute))
                .WithMessage("Website must be a valid URL.");
        }
    }
}
