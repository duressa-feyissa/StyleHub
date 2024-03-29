using Application.DTO.Product.CategoryDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Category.Requests.Commands
{
    public class DeleteCategoryRequest : IRequest<BaseResponse<CategoryResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}