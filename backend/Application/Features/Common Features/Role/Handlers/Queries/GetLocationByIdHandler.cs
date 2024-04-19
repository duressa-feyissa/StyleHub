using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Role.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Handlers.Queries
{
    public class GetRoleByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetRoleById, RoleResponseDTO>
    {
        public async Task<RoleResponseDTO> Handle(
            GetRoleById request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Role = await unitOfWork.RoleRepository.GetById(request.Id);
            if (Role == null)
            {
                throw new NotFoundException("Role with that {request.Id} does not exist");
            }
            var RoleResponse = mapper.Map<RoleResponseDTO>(Role);
            return RoleResponse;
        }
    }
}
