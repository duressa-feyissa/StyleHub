using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Requests.Commands
{
    public class DeleteCategoryRequest : IRequest<BaseResponse<CategoryResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}