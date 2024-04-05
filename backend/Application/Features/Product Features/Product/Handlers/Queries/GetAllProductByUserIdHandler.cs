using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ProductDTO.DTO;
using Application.Features.Product_Features.Product.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Product.Handlers.Queries
{
    public class GetAllProductUserIdHandler
        : IRequestHandler<GetAllProductUserId, List<ProductResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductResponseDTO>> Handle(
            GetAllProductUserId request,
            CancellationToken cancellationToken
        )
        {
            var products = await _unitOfWork.ProductRepository.GetByUserId(
                userId: request.UserId,
                skip: request.Skip,
                limit: request.Limit
            );
            var productResponse = _mapper.Map<List<ProductResponseDTO>>(products);
            return productResponse;
        }
    }
}
