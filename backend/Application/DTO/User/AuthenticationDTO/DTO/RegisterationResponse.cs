namespace Application.DTO.User.AuthenticationDTO.Validations
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
