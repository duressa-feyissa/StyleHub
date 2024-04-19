using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Role.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Handlers.Commands
{
    public class UpdateRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateRoleRequest, BaseResponse<RoleResponseDTO>>
    {
        public async Task<BaseResponse<RoleResponseDTO>> Handle(
            UpdateRoleRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingRole = await unitOfWork.RoleRepository.GetById(request.Id);

            if (existingRole == null)
                throw new NotFoundException("Role Not Found");

            if (request?.Role?.Name != null)
            {
                var existingRoleName = await unitOfWork.RoleRepository.GetByName(
                    request?.Role?.Name ?? ""
                );
                if (existingRoleName != null)
                    throw new BadRequestException("Role Name Already Exists");
                if (request?.Role?.Name.Length < 3)
                    throw new BadRequestException("Role Name Must Be At Least 3 Characters");
                existingRole.Name = request?.Role?.Name ?? "";
            }

            if (request?.Role?.Description != null)
            {
                if (request?.Role?.Description.Length < 3)
                    throw new BadRequestException("Role Description Must Be At Least 3 Characters");
                existingRole.Description = request?.Role?.Description ?? "";
            }

            if (request?.Role?.Code != null)
            {
                if (request?.Role?.Code.Length < 3)
                    throw new BadRequestException("Role Code Must Be At Least 3 Characters");
                existingRole.Code = request?.Role?.Code ?? "";
            }

            existingRole.UpdatedAt = DateTime.Now;
            await unitOfWork.RoleRepository.Update(existingRole);
            return new BaseResponse<RoleResponseDTO>
            {
                Message = "Role Updated Successfully",
                Success = true,
                Data = mapper.Map<RoleResponseDTO>(existingRole)
            };
        }
    }
}
