using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries
{
    public class GetAllProductUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllProductUserId, List<ProductResponseDTO>>
    {
        public async Task<List<ProductResponseDTO>> Handle(
            GetAllProductUserId request,
            CancellationToken cancellationToken
        )
        {
            var products = await unitOfWork.ProductRepository.GetByUserId(
                userId: request.UserId,
                skip: request.Skip,
                limit: request.Limit
            );
            var productResponse = mapper.Map<List<ProductResponseDTO>>(products);
            return productResponse;
        }
    }
}
