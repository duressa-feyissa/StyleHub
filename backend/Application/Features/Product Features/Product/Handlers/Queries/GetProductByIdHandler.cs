using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries
{
    public class GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetProductById, ProductResponseDTO>
    {
        public async Task<ProductResponseDTO> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var product = await unitOfWork.ProductRepository.GetById(request.Id);
            var productResponse = mapper.Map<ProductResponseDTO>(product);
            return productResponse;
        }

    }
}