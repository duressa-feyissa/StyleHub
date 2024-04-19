using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Requests.Commands
{
    public class DeleteRoleRequest : IRequest<BaseResponse<RoleResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}