using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Role.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Handlers.Queries
{
    public class GetAllRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllRole, List<RoleResponseDTO>>
    {
        public async Task<List<RoleResponseDTO>> Handle(
            GetAllRole request,
            CancellationToken cancellationToken
        )
        {
            var Roles = await unitOfWork.RoleRepository.GetAll();
            if (Roles == null)
            {
                throw new NotFoundException("No Roles found");
            }
            var RoleResponse = mapper.Map<List<RoleResponseDTO>>(Roles);
            return RoleResponse;
        }
    }
}
