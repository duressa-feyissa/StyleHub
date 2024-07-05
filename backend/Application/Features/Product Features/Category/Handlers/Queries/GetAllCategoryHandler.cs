using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Category.Requests.Queries;
using MediatR;
using Newtonsoft.Json;

namespace backend.Application.Features.Product_Features.Category.Handlers.Queries
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategory, List<CategoryResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseDTO>> Handle(GetAllCategory request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();
            if (categories == null)
            {
                throw new NotFoundException("No categories found");
            }

            var categoryResponse = new List<CategoryResponseDTO>();
            foreach (var category in categories)
            {
                categoryResponse.Add(new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Image = category.Image,
                    Domain = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(category.Domain)
                });
            }
            return categoryResponse;
        }
    }
}