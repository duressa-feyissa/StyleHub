using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.UserDTO.DTO;
using Application.Exceptions;
using Application.Features.User_Features.User.Requests.Command;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.User_Features.User.Handlers.Command
{
    public class DeleteUserProfileHandler
        : IRequestHandler<DeleteUserProfileRequest, BaseResponse<UserResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserResponseDTO>> Handle(
            DeleteUserProfileRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            await _unitOfWork.UserRepository.Delete(user);

            return new BaseResponse<UserResponseDTO>
            {
                Data = _mapper.Map<UserResponseDTO>(user),
                Message = "User deleted successfully",
                Success = true
            };
        }
    }
}
