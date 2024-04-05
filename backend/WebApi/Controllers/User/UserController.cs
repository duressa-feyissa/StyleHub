using System.Security.Claims;
using Application.DTO.User.UserDTO.DTO;
using Application.Features.User_Features.User.Requests.Command;
using Application.Features.User_Features.User.Requests.Commands;
using Application.Features.User_Features.User.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUserRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var request = new GetUserByIdRequest { Id = id };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var request = new GetUserByIdRequest { Id = userId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var request = new DeleteUserProfileRequest { Id = userId };
            await _mediator.Send(request);
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
            var result = await _mediator.Send(updateRequest);
            return Ok(result);
        }
    }
}
