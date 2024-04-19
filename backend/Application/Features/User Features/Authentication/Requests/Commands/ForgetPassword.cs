using backend.Application.DTO.User.AuthenticationDTO.DTO;
using MediatR;

namespace backend.Application.Features.User_Features.Authentication.Requests.Commands
{
    public class ForgetPasswordRequest : IRequest<ForgetPasswordResponse>
    {
        public required string Email { get; set; }
    }
}
