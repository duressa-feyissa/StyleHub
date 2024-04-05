using Application.DTO.Product.CategoryDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Category.Requests.Queries
{
    public class GetCategoryById : IRequest<CategoryResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}