using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.User.UserDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.User_Features.User.Requests.Queries;
using MediatR;

namespace backend.Application.Features.User_Features.User.Handlers.Queries
{
    public class GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllUserRequest, List<UserResponseDTO>>
    {
        public async Task<List<UserResponseDTO>> Handle(
            GetAllUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var users = await unitOfWork.UserRepository.GetAll(
                request.Skip,
                request.Limit,
                request.Search,
                request.SortBy,
                request.OrderBy,
                request.IsVerified
            );
            if (users == null)
            {
                throw new NotFoundException("No Users found");
            }
            var userResponse = mapper.Map<List<UserResponseDTO>>(users);
            return userResponse;
        }
    }
}
