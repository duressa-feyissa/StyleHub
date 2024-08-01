using backend.Application.DTO.Common.Role.DTO;

namespace backend.Application.DTO.User.UserDTO.DTO
{
	public class UserResponseDTO
	{
		public required string Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public string? Latitude { get; set; }
		public string? Longitude { get; set; }
		public string? ProfilePicture { get; set; }
		public string? Street { get; set; }
		public string? SubLocality { get; set; }
		public string? SubAdministrativeArea { get; set; }
		public string? PostalCode { get; set; }
		public required RoleResponseDTO Role { get; set; }
	}
}
