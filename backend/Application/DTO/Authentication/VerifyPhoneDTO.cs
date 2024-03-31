namespace Application.DTO.Authentication
{
    public class VerifyPhoneDTO
    {
        public required string PhoneNumber { get; set; }
        public required string Code { get; set; }
    }
}
