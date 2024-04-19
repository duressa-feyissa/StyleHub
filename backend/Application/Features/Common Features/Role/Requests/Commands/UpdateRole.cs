using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Requests.Commands
{
    public class UpdateRoleRequest : IRequest<BaseResponse<RoleResponseDTO>>
    {
        public string Id { get; set; } = "";
        public UpdateRoleDTO? Role { get; set; }
    }
}