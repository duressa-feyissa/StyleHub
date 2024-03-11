
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.Features.Requests.Commands;
using StyleHub.Application.Features.Requests.Queries;

namespace SytleHub.WebApi.Controllers
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
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserById(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponseDTO>> DeleteUser(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { userId = id });
            return Ok(result);
        }
    }

}