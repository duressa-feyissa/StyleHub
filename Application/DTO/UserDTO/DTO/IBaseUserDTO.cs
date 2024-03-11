namespace StyleHub.Application.DTO.UserDTO.DTO
{
    public interface IBaseUserDTO
    {
        string Id { get; set; }
        string Email { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
