namespace backend.Application.DTO.User.AuthenticationDTO.DTO
{
    public class RegisterationResponseDTO
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
