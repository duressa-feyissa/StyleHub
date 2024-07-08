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
            [FromQuery] IEnumerable<string>? colorIds = null,
            [FromQuery] IEnumerable<string>? materialIds = null,
            [FromQuery] IEnumerable<string>? sizeIds = null,
            [FromQuery] IEnumerable<string>? brandIds = null,
            [FromQuery] IEnumerable<string>? designIds = null,
            [FromQuery] IEnumerable<string>? categoryIds = null,
            [FromQuery] bool? isNegotiable = null,
            [FromQuery] float? minPrice = null,
            [FromQuery] float? maxPrice = null,
            [FromQuery] int? minQuantity = null,
            [FromQuery] int? maxQuantity = null,
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await mediator.Send(
                new GetAllProduct
                {
                    Search = search,
                    ColorIds = colorIds,
                    BrandIds = brandIds,
                    DesignIds = designIds,
                    MaterialIds = materialIds,
                    SizeIds = sizeIds,
                    CategoryIds = categoryIds,
                    UserId = userId,
                    IsNegotiable = isNegotiable,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinQuantity = minQuantity,
                    MaxQuantity = maxQuantity,
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
        
        [HttpGet("favourite")]
        [Authorize]
        public async Task<ActionResult<List<ProductResponseDTO>>> GetFavouriteProducts(
            [FromQuery] int skip = 0,
            [FromQuery] int limit = 10
        )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await mediator.Send(
                new GetFavouriteProduct(userId, skip, limit)
            );

            return Ok(result);
        }
        
        [HttpPost("favourite/{productId}")]
        [Authorize]
        public async Task<ActionResult<string>> AddOrRemoveFavouriteProduct(string productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await mediator.Send(
                new AddOrRemoveFavouriteProduct { UserId = userId, ProductId = productId }
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
