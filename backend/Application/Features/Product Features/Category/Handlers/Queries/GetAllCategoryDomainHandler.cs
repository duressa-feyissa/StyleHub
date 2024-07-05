using backend.Application.Features.Product_Features.Category.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Handlers.Queries
{
    public class GetAllCategoryDomainHandler : IRequestHandler<GetAllCategoryDomain, List<Dictionary<string, List<string>>>>
    {
        public Task<List<Dictionary<string, List<string>>>> Handle(GetAllCategoryDomain request, CancellationToken cancellationToken)
        {
            var validDomains = new List<Dictionary<string, List<string>>>
            {
                new Dictionary<string, List<string>> { { "men", new List<string> { "tops", "casual", "formal", "outerwear", "sportswear", "accessories", "shoes", "suits", "jeans", "shorts", "swimwear", "underwear" } } },
                new Dictionary<string, List<string>> { { "women", new List<string> { "tops", "casual", "formal", "outerwear", "sportswear", "accessories", "shoes", "dresses", "skirts", "blouses", "jeans", "leggings", "shorts", "swimwear", "lingerie" } } },
                new Dictionary<string, List<string>> { { "kids", new List<string> { "tops", "casual", "playwear", "outerwear", "sportswear", "accessories", "shoes", "dresses", "skirts", "jeans", "shorts", "swimwear", "pajamas" } } },
                new Dictionary<string, List<string>> { { "unisex", new List<string> { "tops", "casual", "outerwear", "sportswear", "accessories", "shoes", "hoodies", "sweatpants" } } },
                new Dictionary<string, List<string>> { { "accessories", new List<string> { "hats", "belts", "scarves", "gloves", "jewelry", "sunglasses", "watches", "bags", "wallets" } } },
                new Dictionary<string, List<string>> { { "shoes", new List<string> { "sneakers", "boots", "sandals", "formal", "casual", "loafers", "heels", "flats", "running", "hiking" } } },
                new Dictionary<string, List<string>> { { "activewear", new List<string> { "tops", "leggings", "shorts", "sports bras", "jackets", "accessories", "shoes" } } },
                new Dictionary<string, List<string>> { { "swimwear", new List<string> { "men", "women", "kids" } } },
                new Dictionary<string, List<string>> { { "lingerie", new List<string> { "bras", "panties", "bodysuits", "loungewear", "nightwear" } } }
            };
            return Task.FromResult(validDomains);
        }
    }
}
