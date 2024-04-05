using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Role.DTO;
using Application.DTO.Common.Role.Validations;
using Application.Exceptions;
using Application.Features.Common_Features.Role.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Role.Handlers.Commands
{
    public class CreateRoleHandler
        : IRequestHandler<CreateRoleRequest, BaseResponse<RoleResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<RoleResponseDTO>> Handle(
            CreateRoleRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateRoleValidation(_unitOfWork.RoleRepository);
            var validationResult = await validator.ValidateAsync(request.Role!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var _Role = _mapper.Map<Domain.Entities.Common.Role>(request.Role);
            await _unitOfWork.RoleRepository.Add(_Role);

            return new BaseResponse<RoleResponseDTO>
            {
                Message = "Role Created Successfully",
                Success = true,
                Data = _mapper.Map<RoleResponseDTO>(_Role)
            };
        }
    }
}
