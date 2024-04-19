namespace backend.Application.DTO.User.UserDTO.DTO
{
    public class UpdateUserProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? ProfilePictureBase64 { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
    }
}
