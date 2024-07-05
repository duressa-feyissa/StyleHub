using AutoMapper;
using backend.Application.Contracts.Infrastructure.Repositories;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.DTO.Product.CategoryDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Category.Requests.Commands;
using backend.Application.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace backend.Application.Features.Product_Features.Category.Handlers.Commands
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, BaseResponse<CategoryResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public CreateCategoryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageRepository imageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public async Task<BaseResponse<CategoryResponseDTO>> Handle(
            CreateCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var validator = new BaseCategoryValidation(_unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.Category!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var category = _mapper.Map<Domain.Entities.Product.Category>(request.Category);
            category.Image = await _imageRepository.Upload(request.Category.Image, category.Id);
            category.Domain = JsonSerializer.Serialize(request.Category.Domain);

            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.Save();

            return new BaseResponse<CategoryResponseDTO>
            {
                Message = "Category Created Successfully",
                Success = true,
                Data =  new CategoryResponseDTO 
				{
					Id = category.Id,
					Name = category.Name,
					Image = category.Image,
                    Domain = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(category.Domain)
				}
            };
        }
    }
}
