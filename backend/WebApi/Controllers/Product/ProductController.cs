using System.Security.Claims;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Features.Product_Features.Product.Requests.Commands;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
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
            [FromQuery] double? latitudes = null,
            [FromQuery] double? longitudes = null,
            [FromQuery] double? radiusInKilometers = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortOrder = null,
            [FromQuery] int skip = 0,
            [FromQuery] int limit = 15
        )
        {
            var result = await mediator.Send(
                new GetAllProduct
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
                    Latitude = latitudes,
                    Longitude = longitudes,
                    RadiusInKilometers = radiusInKilometers,
                    SortBy = sortBy,
                    SortOrder = sortOrder,
                    Skip = skip,
                    Limit = limit
                }
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> fetchProductById(string id)
        {
            var result = await mediator.Send(new GetProductById { Id = id });
            return Ok(result);
        }

        [HttpGet("Added-By-User")]
        [Authorize]
        public async Task<ActionResult<ProductResponseDTO>> fetchProductByUser(
            [FromQuery] int Skip = 0,
            [FromQuery] int Limit = 10
        )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await mediator.Send(
                new GetAllProductUserId
                {
                    UserId = userId,
                    Limit = Limit,
                    Skip = Skip
                }
            );
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct(
            [FromBody] CreateProductDTO product
        )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await mediator.Send(
                new CreateProductRequest { Product = product, UserId = userId }
            );

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResponseDTO>> DeleteProductRequest(string id)
        {
            var result = await mediator.Send(new DeleteProductRequest { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResponseDTO>> UpdateProductRequest(
            string id,
            [FromBody] UpdateProductDTO product
        )
        {
            var result = await mediator.Send(
                new UpdateProductRequest { Id = id, Product = product }
            );
            return Ok(result);
        }
    }
}
