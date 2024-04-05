using Application.DTO.Product.CategoryDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Category.Requests.Commands
{
    public class CreateCategoryRequest : IRequest<BaseResponse<CategoryResponseDTO>>
    {
        public CreateCategoryDTO? Category { get; set; }
    }
}