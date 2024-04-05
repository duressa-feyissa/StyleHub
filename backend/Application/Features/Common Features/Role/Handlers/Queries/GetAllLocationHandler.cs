using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Role.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Role.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Role.Handlers.Queries
{
    public class GetAllRoleHandler : IRequestHandler<GetAllRole, List<RoleResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RoleResponseDTO>> Handle(
            GetAllRole request,
            CancellationToken cancellationToken
        )
        {
            var Roles = await _unitOfWork.RoleRepository.GetAll();
            if (Roles == null)
            {
                throw new NotFoundException("No Roles found");
            }
            var RoleResponse = _mapper.Map<List<RoleResponseDTO>>(Roles);
            return RoleResponse;
        }
    }
}
