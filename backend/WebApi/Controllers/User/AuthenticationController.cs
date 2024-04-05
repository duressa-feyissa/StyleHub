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

		[HttpPost("Register")]
		public async Task<IActionResult> Register(
			[FromBody] RegisterUserRequest registerUserCommand
		)
		{
			var result = await _mediator.Send(registerUserCommand);
			return Ok(result);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserCommand)
		{
			var result = await _mediator.Send(loginUserCommand);
			return Ok(result);
		}

		[HttpPost("Send-Verfication-Email-Code")]
		public async Task<IActionResult> SendOTPForEmail([FromBody] EmailOTPSenderRequest email)
		{
			var result = await _mediator.Send(email);
			return Ok(result);
		}

		[HttpPost("Verify-Email")]
		public async Task<IActionResult> VerifyEmail(
			[FromBody] VerifyEmailRequest verifyEmailCommand
		)
		{
			var result = await _mediator.Send(verifyEmailCommand);
			return Ok(result);
		}
		
		[HttpPost("Send-Reset-Password-Code")]
		public async Task<IActionResult> SendResetPasswordCode(
			[FromBody] ForgetPasswordRequest sendResetPasswordCodeCommand
		)
		{
			var result = await _mediator.Send(sendResetPasswordCodeCommand);
			return Ok(result);
		}
		
		[HttpPost("Reset-Password")]
		public async Task<IActionResult> ResetPassword(
			[FromBody] ResetPasswordRequest resetPasswordCommand
		)
		{
			var result = await _mediator.Send(resetPasswordCommand);
			return Ok(result);
		}
	}
}
