using backend.Application.DTO.Product.CategoryDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Requests.Queries
{
    public class GetAllCategoryDomain : IRequest<List<Dictionary<string, List<string>>>>
    {
    }
}
