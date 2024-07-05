using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.Product.CategoryDTO.Validations
{
    public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryValidation(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name cannot be empty")
                .Custom((name, context) => context.InstanceToValidate.Name = name.ToLower());

            RuleFor(x => x.Image)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Image is required")
                .NotEmpty()
                .WithMessage("Image cannot be empty");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .MustAsync(async (name, cancellation) =>
                {
                    var category = await _categoryRepository.GetByName(name);
                    return category == null;
                })
                .WithMessage("Category already exists");

            RuleFor(x => x.Domain)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Domain is required")
                .Must(BeAValidDomain)
                .WithMessage("Domain must contain valid entries");
        }

        private bool BeAValidDomain(Dictionary<string, List<string>> domain)
        {
            var validDomains = new Dictionary<string, List<string>>
            {
                { "men", new List<string> { "tops", "casual", "formal", "outerwear", "sportswear", "accessories", "shoes", "suits", "jeans", "shorts", "swimwear", "underwear" } },
                { "women", new List<string> { "tops", "casual", "formal", "outerwear", "sportswear", "accessories", "shoes", "dresses", "skirts", "blouses", "jeans", "leggings", "shorts", "swimwear", "lingerie" } },
                { "kids", new List<string> { "tops", "casual", "playwear", "outerwear", "sportswear", "accessories", "shoes", "dresses", "skirts", "jeans", "shorts", "swimwear", "pajamas" } },
                { "unisex", new List<string> { "tops", "casual", "outerwear", "sportswear", "accessories", "shoes", "hoodies", "sweatpants" } },
                { "accessories", new List<string> { "hats", "belts", "scarves", "gloves", "jewelry", "sunglasses", "watches", "bags", "wallets" } },
                { "shoes", new List<string> { "sneakers", "boots", "sandals", "formal", "casual", "loafers", "heels", "flats", "running", "hiking" } },
                { "activewear", new List<string> { "tops", "leggings", "shorts", "sports bras", "jackets", "accessories", "shoes" } },
                { "swimwear", new List<string> { "men", "women", "kids" } },
                { "lingerie", new List<string> { "bras", "panties", "bodysuits", "loungewear", "nightwear" } }
            };

            return domain.All(entry =>
            {
                var key = entry.Key;
                var values = entry.Value;
                if (!validDomains.ContainsKey(key))
                {
                    return false;
                }
                return values.All(value => validDomains[key].Contains(value));
            });
        }
    }
}
