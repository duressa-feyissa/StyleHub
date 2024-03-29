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
    public class DeleteCategoryHandler
        : IRequestHandler<DeleteCategoryRequest, BaseResponse<CategoryResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageUploadRepository _imageUploadRepository;

        public DeleteCategoryHandler(
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
            DeleteCategoryRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id.Length == 0)
                throw new BadRequestException("Invalid Category Id");

            var Category = await _unitOfWork.CategoryRepository.GetById(request.Id);

            if (Category == null)
                throw new NotFoundException("Category Not Found");

            var isDeleted = await _imageUploadRepository.Delete(Category.Id);
            if (!isDeleted)
                throw new BadRequestException("Failed to delete Category");

            await _unitOfWork.CategoryRepository.Delete(Category);

            return new BaseResponse<CategoryResponseDTO>
            {
                Message = "Category Deleted Successfully",
                Success = true,
                Data = _mapper.Map<CategoryResponseDTO>(Category)
            };
        }
    }
}
