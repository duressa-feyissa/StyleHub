using StyleHub.Application.DTO.UserDTO.DTO;

namespace SocialSync.Application.DTO.UserDTO.DTO
{
    public class UserResponseDTO : IBaseUserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}