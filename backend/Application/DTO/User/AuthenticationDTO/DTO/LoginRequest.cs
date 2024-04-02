namespace Application.DTO.User.AuthenticationDTO.DTO
{
	public class LoginRequestDTO
	{
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
