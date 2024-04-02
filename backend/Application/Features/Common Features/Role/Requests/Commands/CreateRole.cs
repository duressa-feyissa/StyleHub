using Application.DTO.Common.Role.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Common_Features.Role.Requests.Commands
{
    public class CreateRoleRequest : IRequest<BaseResponse<RoleResponseDTO>>
    {
        public CreateRoleDTO? Role { get; set; }
    }
}
