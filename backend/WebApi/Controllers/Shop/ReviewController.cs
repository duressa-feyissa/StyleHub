using backend.Application.DTO.Shop.ReviewDTO.DTO;
using backend.Application.Features.Shop_Features.Review.Requests.Commands;
using backend.Application.Features.Shop_Features.Review.Requests.Queries;

namespace backend.WebApi.Controllers.Shop;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReviewController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReviewResponseDTO>> CreateReview([FromBody] CreateReviewDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new CreateReviewRequest()
        {
            UserId = userId!,
            Review = request
        });
        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<ReviewResponseDTO>> UpdateReview([FromBody] UpdateReviewDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new UpdateReviewRequest()
        {
            UserId = userId!,
            Review = request
        });
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ReviewResponseDTO>> DeleteReview(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new DeleteReviewRequest()
        {
            UserId = userId!,
            Id =id,
        });
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ReviewResponseDTO>>> FetchAllReviews(
        [FromQuery] string? shopId = null,
        [FromQuery] string? userId = null,
        [FromQuery] int? rating = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortOrder = null,
        [FromQuery] int skip = 0,
        [FromQuery] int limit = 10
    )
    {
        var result = await mediator.Send(new GetAllReviewRequest
        {
            ShopId = shopId,
            UserId = userId,
            Rating = rating,
            SortBy = sortBy,
            SortOrder = sortOrder,
            Skip = skip,
            Limit = limit
        });
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewResponseDTO>> FetchReviewById(string id)
    {
        var result = await mediator.Send(new GetReviewByIdRequest
        {
            Id = id
        });
        return Ok(result);
    }
}