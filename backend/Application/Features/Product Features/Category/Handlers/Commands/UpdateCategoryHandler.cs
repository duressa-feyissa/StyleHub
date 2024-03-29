using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.CategoryDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Category.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Category.Handlers.Commands
{
    public class UpdateCategoryHandler
        : IRequestHandler<UpdateCategoryRequest, BaseResponse<CategoryResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageUploadRepository _imageUploadRepository;

        public UpdateCategoryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageUploadRepository imageUploadRepository
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageUploadRepository = imageUploadRepository;
        }

        public async Task<BaseResponse<CategoryResponseDTO>> Handle(
            UpdateCategoryRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetById(request.Id);
            if (existingCategory == null)
                throw new NotFoundException("Category Not Found");

            if (request?.Category?.Name != null)
            {
                var existingCategoryName = await _unitOfWork.CategoryRepository.GetByName(
                    request.Category.Name
                );
                if (existingCategoryName != null && existingCategoryName.Id != request.Id)
                    throw new BadRequestException("Category Name Already Exists");
                if (request.Category.Name.Length == 0)
                    throw new BadRequestException("Category Name Cannot Be Empty");
                existingCategory.Name = request.Category.Name.Trim().ToLower();
            }

            if (request?.Category?.Image != null)
                existingCategory.Image = await _imageUploadRepository.Update(
                    request.Category.Image,
                    existingCategory.Id
                );

            existingCategory.UpdatedAt = DateTime.Now;

            await _unitOfWork.CategoryRepository.Update(existingCategory);
            return new BaseResponse<CategoryResponseDTO>
            {
                Message = "Category Updated Successfully",
                Success = true,
                Data = _mapper.Map<CategoryResponseDTO>(existingCategory)
            };
        }
    }
}
