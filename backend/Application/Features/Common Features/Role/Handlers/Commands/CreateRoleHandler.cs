using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Role.DTO;
using backend.Application.DTO.Common.Role.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Role.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Handlers.Commands
{
    public class CreateRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateRoleRequest, BaseResponse<RoleResponseDTO>>
    {
        public async Task<BaseResponse<RoleResponseDTO>> Handle(
            CreateRoleRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateRoleValidation(unitOfWork.RoleRepository);
            var validationResult = await validator.ValidateAsync(request.Role!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var _Role = mapper.Map<Domain.Entities.Common.Role>(request.Role);
            await unitOfWork.RoleRepository.Add(_Role);

            return new BaseResponse<RoleResponseDTO>
            {
                Message = "Role Created Successfully",
                Success = true,
                Data = mapper.Map<RoleResponseDTO>(_Role)
            };
        }
    }
}
