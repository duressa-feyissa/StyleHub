using System.Security.Claims;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Features.Shop_Features.Shop.Requests.Commands;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using backend.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Shop;

[ApiController]
[Route("api/[controller]")]
public class ShopController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ShopResponseDTO>> CreateShop([FromBody] CreateShopDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new CreateShopRequest()
        {
            UserId = userId!,
            Shop = request
        });
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<ShopResponseDTO>>> FetchAllShops(
        [FromQuery] string? search = null,
        [FromQuery] List<string>? category = null,
        [FromQuery] double? radiusInKilometers = null,
        [FromQuery] double? latitude = null,
        [FromQuery] double? longitude = null,
        [FromQuery] int? rating = null,
        [FromQuery] bool? verified = null,
        [FromQuery] bool? active = null,
        [FromQuery] string? ownerId = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortOrder = null,
        [FromQuery] int skip = 0,
        [FromQuery] int limit = 10
    )
    {
        var result = await mediator.Send(new GetAllShopRequest
        {
            Search = search,
            Category = category,
            RadiusInKilometers = radiusInKilometers,
            Latitude = latitude,
            Longitude = longitude,
            Rating = rating,
            Verified = verified,
            Active = active,
            OwnerId = ownerId,
            SortBy = sortBy,
            SortOrder = sortOrder,
            Skip = skip,
            Limit = limit
        });
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ShopResponseDTO>> FetchShopById(string id)
    {
        var result = await mediator.Send(new GetShopByIdRequest
        {
            Id = id
        });
        
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ShopResponseDTO>> UpdateShop(string id, [FromBody] UpdateShopDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        request.Id = id;
        var result = await mediator.Send(new UpdateShopRequest
        {
            UserId = userId!,
            Shop = request
        });
        
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteShop(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new DeleteShopRequest
        {
            UserId = userId!,
            Id = id
        });
        
        return Ok(result);
    }

    [HttpGet("{id}/products/images")]
    public async Task<ActionResult<ImageResponseDTO>> ShopImages(string id, [FromQuery] int skip = 0,
        [FromQuery] int limit = 10)
    {
        var result = await mediator.Send(new GetShopProductImagesRequest(id, skip, limit));
        return Ok(result);
    }
    
    [HttpGet("{id}/products/videos")]
    public async Task<ActionResult<ImageResponseDTO>> ShopVideos(string id, [FromQuery] int skip = 0,
        [FromQuery] int limit = 10)
    {
        var result = await mediator.Send(new GetShopProductVideosRequest(id, skip, limit));
        return Ok(result);
    }
    
    [HttpGet("products/followed")]
    [Authorize]
    public async Task<ActionResult<List<ProductResponseDTO>>> ShopProductsFollowedByUser([FromQuery] int skip = 0,
        [FromQuery] int limit = 10)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new GetShopProductFollowedByUser
        {
            UserId = userId!,
            Skip = skip,
            Limit = limit
        });
        return Ok(result);
    }
    
    [HttpGet("followed")]
    [Authorize]
    public async Task<ActionResult<List<ShopResponseDTO>>> ShopFollowedByUser([FromQuery] int skip = 0,
        [FromQuery] int limit = 10)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new GetShopFollowedByUserRequest
        {
            UserId = userId!,
            Skip = skip,
            Limit = limit
        });
        return Ok(result);
    }
    
    [HttpGet("{id}/follow-or-unfollow")]
    [Authorize]
    public async Task<ActionResult<BaseResponse<string>>> FollowOrUnfollowShop(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new FollowShopRequest
        {
            UserId = userId!,
            ShopId = id
        });
        return Ok(result);
    }
    
    [HttpGet("{id}/analytics")]
    [Authorize]
    public async Task<ActionResult<ShopAnalyticsDTO>> ShopAnalytics(string id)
    {
        var result = await mediator.Send(new ShopAnalyticRequest
        {
            ShopId = id
        });
        return Ok(result);
    }
    
    [HttpGet("{id}/is-followed")]
    [Authorize]
    public async Task<ActionResult<bool>> IsShopFollowedByUser(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new CheckIfShopFollowedByUser
        {
            ShopId = id,
            UserId = userId!
        });
        return Ok(result);
    }
    
}