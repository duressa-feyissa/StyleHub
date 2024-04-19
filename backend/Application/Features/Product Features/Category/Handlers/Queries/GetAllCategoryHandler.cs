using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Category.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Category.Handlers.Queries
{
    public class GetAllCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllCategory, List<CategoryResponseDTO>>
    {
        public async Task<List<CategoryResponseDTO>> Handle(
            GetAllCategory request,
            CancellationToken cancellationToken
        )
        {
            var Categorys = await unitOfWork.CategoryRepository.GetAll();
            if (Categorys == null)
            {
                throw new NotFoundException("No Categorys found");
            }
            var CategoryResponse = mapper.Map<List<CategoryResponseDTO>>(Categorys);
            return CategoryResponse;
        }
    }
}
