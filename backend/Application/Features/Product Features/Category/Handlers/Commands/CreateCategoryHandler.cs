using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.CategoryDTO.DTO;
using Application.DTO.Product.CategoryDTO.Validations;
using Application.Exceptions;
using Application.Features.Product_Features.Category.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Category.Handlers.Commands
{
    public class CreateCategoryHandler
        : IRequestHandler<CreateCategoryRequest, BaseResponse<CategoryResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageUploadRepository _imageUploadRepository;

        public CreateCategoryHandler(
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
            CreateCategoryRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new BaseCategoryValidation(_unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.Category!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Category = _mapper.Map<Domain.Entities.Product.Category>(request.Category);
            Category.Image = await _imageUploadRepository.Upload(Category.Image, Category.Id);

            await _unitOfWork.CategoryRepository.Add(Category);

            return new BaseResponse<CategoryResponseDTO>
            {
                Message = "Category Created Successfully",
                Success = true,
                Data = _mapper.Map<CategoryResponseDTO>(Category)
            };
        }
    }
}
