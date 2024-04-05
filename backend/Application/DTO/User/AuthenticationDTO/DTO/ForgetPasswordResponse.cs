namespace Application.DTO.User.AuthenticationDTO.DTO
{
	public class ForgetPasswordResponse
	{
		public required string Email { get; set; }
		public required string Message { get; set; }
		public required string Status { get; set; }
		public required DateTime ExpirationDate { get; set; }
	}
}
