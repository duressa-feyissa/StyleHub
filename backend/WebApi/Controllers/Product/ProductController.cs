using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.Product.ProductDTO.DTO;
using Application.Features.Product_Features.Product.Requests.Queries;
using Application.Features.Product_Features.Product.Requests.Commands;

namespace WebApi.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private IMediator _mediator;

		public ProductController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpGet]
		public async Task<ActionResult<List<ProductResponseDTO>>> FetchAllProducts(
		[FromQuery] string search = "",
		[FromQuery] string? brandId = null,
		[FromQuery] IEnumerable<string>? colorIds = null,
		[FromQuery] IEnumerable<string>? materialIds = null,
		[FromQuery] IEnumerable<string>? sizeIds = null,
		[FromQuery] bool? isNegotiable = null,
		[FromQuery] float? minPrice = null,
		[FromQuery] float? maxPrice = null,
		[FromQuery] int? minQuantity = null,
		[FromQuery] int? maxQuantity = null,
		[FromQuery] string? target = null,
		[FromQuery] string? condition = null,
		[FromQuery] double? latitude = null,
		[FromQuery] double? longitude = null,
		[FromQuery] double? radiusInKilometers = null,
		[FromQuery] int skip = 0,
		[FromQuery] int limit = 15)
		{
			var result = await _mediator.Send(new GetAllProduct
			{
				Search = search,
				BrandId = brandId,
				ColorIds = colorIds,
				MaterialIds = materialIds,
				SizeIds = sizeIds,
				IsNegotiable = isNegotiable,
				MinPrice = minPrice,
				MaxPrice = maxPrice,
				MinQuantity = minQuantity,
				MaxQuantity = maxQuantity,
				Target = target,
				Condition = condition,
				Latitude = latitude,
				Longitude = longitude,
				RadiusInKilometers = radiusInKilometers,
				Skip = skip,
				Limit = limit
			});

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductResponseDTO>> fetchProductById(string id)
		{
			var result = await _mediator.Send(new GetProductById { Id = id });
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] CreateProductRequest command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<ProductResponseDTO>> DeleteProductRequest(string id)
		{
			var result = await _mediator.Send(new DeleteProductRequest { Id = id });
			return Ok(result);
		}
	}
}