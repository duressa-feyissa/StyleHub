using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.User_Features.Authentication.Requests.Commands
{
    public class LoginUserRequest : IRequest<BaseResponse<AuthenticationResponseDTO>>
    {
        public required LoginRequestDTO LoginRequest { get; set; }
    }
}
