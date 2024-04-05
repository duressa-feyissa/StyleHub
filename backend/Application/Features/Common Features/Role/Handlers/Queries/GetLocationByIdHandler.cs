using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Role.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Role.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Role.Handlers.Queries
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleById, RoleResponseDTO>
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public GetRoleByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoleResponseDTO> Handle(
            GetRoleById request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Role = await _unitOfWork.RoleRepository.GetById(request.Id);
            if (Role == null)
            {
                throw new NotFoundException("Role with that {request.Id} does not exist");
            }
            var RoleResponse = _mapper.Map<RoleResponseDTO>(Role);
            return RoleResponse;
        }
    }
}
