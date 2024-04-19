using AutoMapper;
using backend.Application.Contracts.Infrastructure.Repositories;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.DTO.Product.CategoryDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Category.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Handlers.Commands
{
    public class CreateCategoryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IImageRepository imageRepository)
        : IRequestHandler<CreateCategoryRequest, BaseResponse<CategoryResponseDTO>>
    {
        public async Task<BaseResponse<CategoryResponseDTO>> Handle(
            CreateCategoryRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new BaseCategoryValidation(unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.Category!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Category = mapper.Map<Domain.Entities.Product.Category>(request.Category);
            Category.Image = await imageRepository.Upload(Category.Image, Category.Id);

            await unitOfWork.CategoryRepository.Add(Category);

            return new BaseResponse<CategoryResponseDTO>
            {
                Message = "Category Created Successfully",
                Success = true,
                Data = mapper.Map<CategoryResponseDTO>(Category)
            };
        }
    }
}
