using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Role.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Role.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Role.Handlers.Commands
{

    public class DeleteRoleHandler : IRequestHandler<DeleteRoleRequest, BaseResponse<RoleResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<RoleResponseDTO>> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Role Id");
            }

            var Role = await _unitOfWork.RoleRepository.GetById(request.Id);

            if (Role == null)
            {
                throw new  NotFoundException("Role Not Found");
            }

            await _unitOfWork.RoleRepository.Delete(Role);

            return new BaseResponse<RoleResponseDTO>
            {
                Message = "Role Deleted Successfully",
                Success = true,
                Data = _mapper.Map<RoleResponseDTO>(Role)
            };

        }
    }

}