namespace Application.DTO.Authentication
{
	public record AuthenticationResponse
	{
		string Id,
		string PhoneNumber,
		string FirstName,
		string LastName,
		string Token,
	}
}
