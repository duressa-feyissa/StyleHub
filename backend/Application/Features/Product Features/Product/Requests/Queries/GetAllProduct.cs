using backend.Application.DTO.Product.ProductDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Queries
{
	public class GetAllProduct : IRequest<List<ProductResponseDTO>>
	{
		public GetAllProduct(
			string search = "",
			string? brandId = null,
			IEnumerable<string>? colorIds = null,
			IEnumerable<string>? materialIds = null,
			IEnumerable<string>? sizeIds = null,
			IEnumerable<string>? categoryIds = null,
			bool? isNegotiable = null,
			float? minPrice = null,
			float? maxPrice = null,
			int? minQuantity = null,
			int? maxQuantity = null,
			string? target = null,
			string? condition = null,
			double? latitude = null,
			double? longitude = null,
			double? radiusInKilometers = null,
			string? sortBy = null,
			string? sortOrder = null,
			int skip = 0,
			int limit = 10
		)
		{
			Search = search;
			BrandId = brandId;
			ColorIds = colorIds;
			MaterialIds = materialIds;
			SizeIds = sizeIds;
			IsNegotiable = isNegotiable;
			MinPrice = minPrice;
			MaxPrice = maxPrice;
			MinQuantity = minQuantity;
			MaxQuantity = maxQuantity;
			Target = target;
			Condition = condition;
			Latitude = latitude;
			Longitude = longitude;
			RadiusInKilometers = radiusInKilometers;
			SortBy = sortBy;
			SortOrder = sortOrder;
			Skip = skip;
			Limit = limit;
		}

		public string Search { get; set; }
		public string? BrandId { get; set; }
		public IEnumerable<string>? ColorIds { get; set; }
		public IEnumerable<string>? MaterialIds { get; set; }
		public IEnumerable<string>? SizeIds { get; set; }
		public bool? IsNegotiable { get; set; }
		public float? MinPrice { get; set; }
		public float? MaxPrice { get; set; }
		public int? MinQuantity { get; set; }
		public int? MaxQuantity { get; set; }
		public string? Target { get; set; }
		public string? Condition { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public double? RadiusInKilometers { get; set; }
		public string? SortBy { get; set; }
		public string? SortOrder { get; set; }
		public int Skip { get; set; }
		public int Limit { get; set; }
	}
}
