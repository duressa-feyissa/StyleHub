using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Requests.Commands
{
    public class CreateRoleRequest : IRequest<BaseResponse<RoleResponseDTO>>
    {
        public CreateRoleDTO? Role { get; set; }
    }
}
