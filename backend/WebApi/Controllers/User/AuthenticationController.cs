using backend.Application.Features.User_Features.Authentication.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.User
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController(IMediator mediator) : ControllerBase
	{
		[HttpPost("Register")]
		public async Task<IActionResult> Register(
			[FromBody] RegisterUserRequest registerUserCommand
		)
		{
			var result = await mediator.Send(registerUserCommand);
			return Ok(result);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserCommand)
		{
			var result = await mediator.Send(loginUserCommand);
			return Ok(result);
		}

		[HttpPost("Send-Verfication-Email-Code")]
		public async Task<IActionResult> SendOTPForEmail([FromBody] EmailOTPSenderRequest email)
		{
			var result = await mediator.Send(email);
			return Ok(result);
		}

		[HttpPost("Verify-Email")]
		public async Task<IActionResult> VerifyEmail(
			[FromBody] VerifyEmailRequest verifyEmailCommand
		)
		{
			var result = await mediator.Send(verifyEmailCommand);
			return Ok(result);
		}
		
		[HttpPost("Send-Reset-Password-Code")]
		public async Task<IActionResult> SendResetPasswordCode(
			[FromBody] ForgetPasswordRequest sendResetPasswordCodeCommand
		)
		{
			var result = await mediator.Send(sendResetPasswordCodeCommand);
			return Ok(result);
		}
		
		[HttpPost("Reset-Password")]
		public async Task<IActionResult> ResetPassword(
			[FromBody] ResetPasswordRequest resetPasswordCommand
		)
		{
			var result = await mediator.Send(resetPasswordCommand);
			return Ok(result);
		}
	}
}
