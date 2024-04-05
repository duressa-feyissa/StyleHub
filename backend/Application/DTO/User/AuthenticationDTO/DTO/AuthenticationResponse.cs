using Domain.Entities.Common;

namespace Application.DTO.User.AuthenticationDTO.DTO
{
    public class AuthenticationResponseDTO
    {
        public required string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public required Role Role { get; set; }
    }
}
