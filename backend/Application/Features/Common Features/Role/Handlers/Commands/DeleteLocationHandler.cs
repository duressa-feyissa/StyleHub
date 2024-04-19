using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Role.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Handlers.Commands
{

    public class DeleteRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteRoleRequest, BaseResponse<RoleResponseDTO>>
    {
        public async Task<BaseResponse<RoleResponseDTO>> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Role Id");
            }

            var Role = await unitOfWork.RoleRepository.GetById(request.Id);

            if (Role == null)
            {
                throw new  NotFoundException("Role Not Found");
            }

            await unitOfWork.RoleRepository.Delete(Role);

            return new BaseResponse<RoleResponseDTO>
            {
                Message = "Role Deleted Successfully",
                Success = true,
                Data = mapper.Map<RoleResponseDTO>(Role)
            };

        }
    }

}