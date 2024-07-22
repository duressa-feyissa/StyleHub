using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.Features.Shop_Features.WorkingHour.Requests.Commands;
using backend.Application.Features.Shop_Features.WorkingHour.Requests.Queries;
using backend.Application.Response;

namespace backend.WebApi.Controllers.Shop;

[ApiController]
[Route("api/[controller]")]
public class WorkingHourController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<BaseResponse<WorkingHourResponseDTO>>> CreateWorkingHour(
        [FromBody] CreateWorkingHourDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new CreateWorkingHourRequest()
        {
            UserId = userId!,
            WorkingHour = request
        });
        return Ok(result);
    }

    [HttpGet("{shopId}/shop")]
    public async Task<ActionResult<List<WorkingHourResponseDTO>>> FetchAllWorkingHours(
        string shopId
    )
    {
        var result = await mediator.Send(new GetAllWorkingHourOfShopRequest
        {
            ShopId = shopId
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkingHourResponseDTO>> FetchWorkingHourById(string id)
    {
        var result = await mediator.Send(new GetWorkingHourByIdRequest
        {
            Id = id
        });
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<WorkingHourResponseDTO>> UpdateWorkingHour(
        [FromBody] UpdateWorkingHourDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new UpdateWorkingHourRequest
        {
            UserId = userId!,
            WorkingHour = request
        });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<WorkingHourResponseDTO>> DeleteWorkingHour(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await mediator.Send(new DeleteWorkingHourRequest
            {
                UserId = userId!,
                Id = id

            }
        );
        return Ok(result);
    }
}