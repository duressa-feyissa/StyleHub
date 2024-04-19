using AutoMapper;
using backend.Application.Contracts.Infrastructure.Repositories;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Category.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Handlers.Commands
{
    public class DeleteCategoryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IImageRepository imageRepository)
        : IRequestHandler<DeleteCategoryRequest, BaseResponse<CategoryResponseDTO>>
    {
        public async Task<BaseResponse<CategoryResponseDTO>> Handle(
            DeleteCategoryRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id.Length == 0)
                throw new BadRequestException("Invalid Category Id");

            var Category = await unitOfWork.CategoryRepository.GetById(request.Id);

            if (Category == null)
                throw new NotFoundException("Category Not Found");

            var isDeleted = await imageRepository.Delete(Category.Id);
            if (!isDeleted)
                throw new BadRequestException("Failed to delete Category");

            await unitOfWork.CategoryRepository.Delete(Category);

            return new BaseResponse<CategoryResponseDTO>
            {
                Message = "Category Deleted Successfully",
                Success = true,
                Data = mapper.Map<CategoryResponseDTO>(Category)
            };
        }
    }
}
