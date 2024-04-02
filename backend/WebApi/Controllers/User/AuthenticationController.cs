using Application.Features.User_Features.Authentication.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(
			[FromBody] RegisterUserRequest registerUserCommand
		)
		{
			var result = await _mediator.Send(registerUserCommand);
			return Ok(result);
		}
		
		[HttpPost("login")]
		public async Task<IActionResult> Login(
			[FromBody] LoginUserRequest loginUserCommand
		)
		{
			var result = await _mediator.Send(loginUserCommand);
			return Ok(result);
		}
	}
}