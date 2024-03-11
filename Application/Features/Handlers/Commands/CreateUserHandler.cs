using AutoMapper;
using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.Contracts;
using StyleHub.Application.DTO.UserDTO.Validations;
using StyleHub.Application.Features.Requests.Commands;
using SytleHub.Application.Exceptions;
using SytleHub.Application.Response;
using SytleHub.Domain.Entities;

namespace StyleHub.Application.Features.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<UserResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserResponseDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UserCreateValidation(_unitOfWork.UserRepository);
            var validationResult = await validator.ValidateAsync(request.User!);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid User Data");
            }

            var user = _mapper.Map<User>(request.User);
            await _unitOfWork.UserRepository.Add(user);
            var createResponse = new BaseResponse<UserResponseDTO>
            {
                Message = "User Created Successfully",
                IsSuccess = true,
                Value = _mapper.Map<UserResponseDTO>(user)
            };

            return createResponse;
        }
    }
}
