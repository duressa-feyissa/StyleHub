namespace backend.Application.DTO.User.AuthenticationDTO.DTO
{
    public class PhoneDTO
    {
        public required string From { get; set; }
        public required string To { get; set; }
        public required string Body { get; set; }
    }
}
