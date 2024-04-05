namespace Application.DTO.User.AuthenticationDTO.DTO
{
    public class EmailDTO
    {
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public bool IsHtml { get; set; }
        public int Priority { get; set; }
    }
}
