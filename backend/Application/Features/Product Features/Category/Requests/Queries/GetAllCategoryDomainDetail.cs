using backend.Application.DTO.Product.CategoryDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Requests.Queries
{
    public class GetAllCategoryDomainDetail : IRequest<Dictionary<string, Dictionary<string, List<CategoryShareResponseDTO>>>>
    {
    }
}
