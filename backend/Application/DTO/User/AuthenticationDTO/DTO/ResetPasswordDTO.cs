namespace Application.DTO.User.AuthenticationDTO.DTO
{
	public class PasswordResetDTO
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
		public required string Code { get; set; }
	}
}
