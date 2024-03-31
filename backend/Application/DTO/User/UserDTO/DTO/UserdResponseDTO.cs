namespace Application.DTO.User.UserDTO.DTO
{
    public class UpdateResponseDTO
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
    }
}
