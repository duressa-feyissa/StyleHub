using Application.DTO.Common.Role.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Common_Features.Role.Requests.Commands
{
    public class DeleteRoleRequest : IRequest<BaseResponse<RoleResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}