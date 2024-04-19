using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.User_Features.Authentication.Requests.Commands
{
    public class RegisterUserRequest : IRequest<BaseResponse<RegisterationResponseDTO>>
    {
        public required RegisterationRequestDTO Registeration { get; set; }
    }
}
