using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.User.UserDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.User_Features.User.Requests.Queries;
using MediatR;

namespace backend.Application.Features.User_Features.User.Handlers.Queries
{
    public class GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetUserByIdRequest, UserResponseDTO>
    {
        public async Task<UserResponseDTO> Handle(
            GetUserByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.UserRepository.GetById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            return mapper.Map<UserResponseDTO>(user);
        }
    }
}
