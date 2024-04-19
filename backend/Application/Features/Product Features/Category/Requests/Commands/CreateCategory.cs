using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Requests.Commands
{
    public class CreateCategoryRequest : IRequest<BaseResponse<CategoryResponseDTO>>
    {
        public CreateCategoryDTO? Category { get; set; }
    }
}