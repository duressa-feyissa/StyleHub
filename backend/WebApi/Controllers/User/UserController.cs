using System.Security.Claims;
using backend.Application.DTO.User.UserDTO.DTO;
using backend.Application.Features.User_Features.User.Requests.Command;
using backend.Application.Features.User_Features.User.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUserRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var request = new GetUserByIdRequest { Id = id };
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var request = new GetUserByIdRequest { Id = userId };
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var request = new DeleteUserProfileRequest { Id = userId };
            await mediator.Send(request);
            return NoContent();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserProfileDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var updateRequest = new UpdateUserProfileRequest
            {
                Id = userId,
                updateUserProfileDTO = request
            };
            var result = await mediator.Send(updateRequest);
            return Ok(result);
        }
    }
}
