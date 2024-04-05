using Application.DTO.User.UserDTO.DTO;
using MediatR;

namespace Application.Features.User_Features.User.Requests.Queries
{
    public class GetAllUserRequest : IRequest<List<UserResponseDTO>>
    {
        public int Skip { get; set; } = 0;
        public int Limit { get; set; } = 10;
        public string Search { get; set; } = "";
        public string SortBy { get; set; } = "firstName";
        public string OrderBy { get; set; } = "asc";
        public bool IsVerified { get; set; }
    }
}
