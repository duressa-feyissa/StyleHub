using Application.Contracts;
using Application.DTO.ProductDTO.DTO;
using Application.Exceptions;
using Application.Features.Product.Requests.Queries;
using AutoMapper;
using MediatR;



namespace Application.Features.Product.Handlers.Queries
{
    public class GetAllProductHandler : IRequestHandler<GetAllProduct, List<ProductResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductResponseDTO>> Handle(GetAllProduct request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAll();
            if (products == null)
            {
                throw new NotFoundException("No products found");
            }
            var productResponse = _mapper.Map<List<ProductResponseDTO>>(products);
            return productResponse;
        }
    }
}