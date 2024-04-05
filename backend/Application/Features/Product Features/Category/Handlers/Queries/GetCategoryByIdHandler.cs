using MediatR;
using AutoMapper;
using Application.Exceptions;
using Application.DTO.Product.CategoryDTO.DTO;
using Application.Contracts.Persistance.Repositories;
using Application.Features.Product_Features.Category.Requests.Queries;

namespace Application.Features.Product_Features.Category.Handlers.Queries
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, CategoryResponseDTO>
    {

        private readonly IMapper _mapper;


        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDTO> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Category = await _unitOfWork.CategoryRepository.GetById(request.Id);
            if (Category == null)
            {
                throw new NotFoundException("Category with that {request.Id} does not exist");
            }
            var CategoryResponse = _mapper.Map<CategoryResponseDTO>(Category);
            return CategoryResponse;
        }

    }
}