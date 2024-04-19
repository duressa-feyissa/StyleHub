using backend.Application.DTO.Product.CategoryDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Requests.Queries
{
    public class GetAllCategory : IRequest<List<CategoryResponseDTO>>
    {
    }
}
