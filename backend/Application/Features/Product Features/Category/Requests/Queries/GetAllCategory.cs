using Application.DTO.Product.CategoryDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Category.Requests.Queries
{
    public class GetAllCategory : IRequest<List<CategoryResponseDTO>>
    {
    }
}
