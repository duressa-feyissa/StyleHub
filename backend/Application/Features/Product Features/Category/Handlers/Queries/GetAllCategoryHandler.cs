using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.CategoryDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Category.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Category.Handlers.Queries
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategory, List<CategoryResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseDTO>> Handle(
            GetAllCategory request,
            CancellationToken cancellationToken
        )
        {
            var Categorys = await _unitOfWork.CategoryRepository.GetAll();
            if (Categorys == null)
            {
                throw new NotFoundException("No Categorys found");
            }
            var CategoryResponse = _mapper.Map<List<CategoryResponseDTO>>(Categorys);
            return CategoryResponse;
        }
    }
}
