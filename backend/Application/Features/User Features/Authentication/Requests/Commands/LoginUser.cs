using Application.DTO.User.AuthenticationDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.User_Features.Authentication.Requests.Commands
{
    public class LoginUserRequest : IRequest<BaseResponse<AuthenticationResponseDTO>>
    {
        public required LoginRequestDTO LoginRequest { get; set; }
    }
}
