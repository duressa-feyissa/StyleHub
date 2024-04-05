using Application.Response;
using MediatR;

namespace Application.Features.User_Features.Authentication.Requests.Commands
{
    public class EmailOTPSenderRequest : IRequest<BaseResponse<string>>
    {
        public required string Email { get; set; }
    }
}
