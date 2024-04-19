using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.User.UserDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.User_Features.User.Requests.Command;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.User_Features.User.Handlers.Command
{
    public class DeleteUserProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteUserProfileRequest, BaseResponse<UserResponseDTO>>
    {
        public async Task<BaseResponse<UserResponseDTO>> Handle(
            DeleteUserProfileRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.UserRepository.GetById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            await unitOfWork.UserRepository.Delete(user);

            return new BaseResponse<UserResponseDTO>
            {
                Data = mapper.Map<UserResponseDTO>(user),
                Message = "User deleted successfully",
                Success = true
            };
        }
    }
}
