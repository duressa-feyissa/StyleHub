using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ProductDTO.DTO;
using Application.Features.Product_Features.Product.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Product.Handlers.Queries
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

        public async Task<List<ProductResponseDTO>> Handle(
            GetAllProduct request,
            CancellationToken cancellationToken
        )
        {
            var products = await _unitOfWork.ProductRepository.GetAll(
                search: request.Search,
                brandId: request.BrandId,
                colorIds: request.ColorIds,
                materialIds: request.MaterialIds,
                sizeIds: request.SizeIds,
                isNegotiable: request.IsNegotiable,
                minPrice: request.MinPrice,
                maxPrice: request.MaxPrice,
                minQuantity: request.MinQuantity,
                maxQuantity: request.MaxQuantity,
                target: request.Target,
                condition: request.Condition,
                latitude: request.Latitude,
                longitude: request.Longitude,
                radiusInKilometers: request.RadiusInKilometers,
                skip: request.Skip,
                limit: request.Limit
            );
            var productResponse = _mapper.Map<List<ProductResponseDTO>>(products);
            return productResponse;
        }
    }
}
