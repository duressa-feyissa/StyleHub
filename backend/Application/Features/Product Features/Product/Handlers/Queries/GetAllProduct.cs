using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries
{
	public class GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
		: IRequestHandler<GetAllProduct, List<ProductResponseDTO>>
	{
		public async Task<List<ProductResponseDTO>> Handle(
			GetAllProduct request,
			CancellationToken cancellationToken
		)
		{
			var products = await unitOfWork.ProductRepository.GetAll(
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
				sortBy: request.SortBy,
				sortOrder: request.SortOrder,
				skip: request.Skip,
				limit: request.Limit
			);
			var productResponse = mapper.Map<List<ProductResponseDTO>>(products);
			return productResponse;
		}
	}
}
