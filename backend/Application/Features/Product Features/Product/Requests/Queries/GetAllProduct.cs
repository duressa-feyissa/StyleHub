using backend.Application.DTO.Product.ProductDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Queries
{
	public class GetAllProduct(
		string search = "",
		IEnumerable<string>? colorIds = null,
		IEnumerable<string>? materialIds = null,
		IEnumerable<string>? sizeIds = null,
		IEnumerable<string>? categoryIds = null,
		IEnumerable<string>? brandIds = null,
		IEnumerable<string>? designIds = null,
		string? userId = null,
		bool? isNegotiable = null,
		float? minPrice = null,
		float? maxPrice = null,
		string? status = null,
		bool? inStock = null,
		string? shopId = null,
		string? condition = null,
		double? latitude = null,
		double? longitude = null,
		double? radiusInKilometers = null,
		string? sortBy = null,
		string? sortOrder = null,
		int skip = 0,
		int limit = 10)
		: IRequest<List<ProductResponseDTO>>
	{
		public string Search { get; set; } = search;
		public IEnumerable<string>? ColorIds { get; set; } = colorIds;
		public IEnumerable<string>? MaterialIds { get; set; } = materialIds;
		public IEnumerable<string>? SizeIds { get; set; } = sizeIds;
		public IEnumerable<string>? CategoryIds { get; set; } = categoryIds;
		public IEnumerable<string>? BrandIds { get; set; } = brandIds;
		public IEnumerable<string>? DesignIds { get; set; } = designIds;
		public string? UserId { get; set; } = userId;
		public bool? IsNegotiable { get; set; } = isNegotiable;
		public float? MinPrice { get; set; } = minPrice;
		public float? MaxPrice { get; set; } = maxPrice;
		public string? Status { get; set; } = status;
		public bool? InStock { get; set; } = inStock;
		public string? ShopId { get; set; } = shopId;
		public string? Condition { get; set; } = condition;
		public double? Latitude { get; set; } = latitude;
		public double? Longitude { get; set; } = longitude;
		public double? RadiusInKilometers { get; set; } = radiusInKilometers;
		public string? SortBy { get; set; } = sortBy;
		public string? SortOrder { get; set; } = sortOrder;
		public int Skip { get; set; } = skip;
		public int Limit { get; set; } = limit;
	}
}
