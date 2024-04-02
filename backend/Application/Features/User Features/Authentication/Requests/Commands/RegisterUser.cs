using Application.DTO.User.AuthenticationDTO.Validations;
using Application.Response;
using MediatR;

namespace Application.Features.User_Features.Authentication.Requests.Commands
{
    public class RegisterUserRequest : IRequest<BaseResponse<RegisterationResponseDTO>>
    {
        public required RegisterationRequestDTO Registeration { get; set; }
    }
}
