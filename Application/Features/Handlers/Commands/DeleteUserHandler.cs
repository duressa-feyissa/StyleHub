
using AutoMapper;
using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.Contracts;
using StyleHub.Application.Features.Requests.Commands;
using SytleHub.Application.Exceptions;
using SytleHub.Application.Response;

namespace StyleHub.Application.Features.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse<UserResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserResponseDTO>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.userId == null)
            {
                throw new BadRequestException("Invalid User Id");
            }
            var user = await _unitOfWork.UserRepository.GetUserById(request.userId);
            if (user == null)
            {
                throw new NotFoundException("User Not Found");
            }

            await _unitOfWork.UserRepository.Delete(user);
            
            var deleteResponse = new BaseResponse<UserResponseDTO>
            {
                Message = "User Deleted Successfully",
                IsSuccess = true,
                Value = _mapper.Map<UserResponseDTO>(user)
            };
            return deleteResponse;
        }
    }
}