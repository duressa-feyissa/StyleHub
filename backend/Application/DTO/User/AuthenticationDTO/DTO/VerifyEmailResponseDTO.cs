namespace Application.DTO.User.AuthenticationDTO.DTO
{
	public class VerifyEmailResponseDTO
	{
		public required string Email { get; set; }
		public required bool IsVerified { get; set; }
		public required string Message { get; set; }
		public required DateTime VerificationDate { get; set; }
		public required string Token { get; set; }
	}
}
