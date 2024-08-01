namespace backend.Application.DTO.User.UserDTO.DTO
{
    public class UpdateUserProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public  string? Street { get; set; }
        public string? SubLocality { get; set; }
        public string? SubAdministrativeArea { get; set; }
        public string? PostalCode { get; set; }
        public string? ProfilePictureBase64 { get; set; }
        public string? Password { get; set; }
    }
}
