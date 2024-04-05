using Application.DTO.Product.CategoryDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Category.Requests.Commands
{
    public class UpdateCategoryRequest : IRequest<BaseResponse<CategoryResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateCategoryDTO? Category { get; set; }
    }
}