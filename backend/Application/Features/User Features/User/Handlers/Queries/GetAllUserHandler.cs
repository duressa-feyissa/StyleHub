using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.UserDTO.DTO;
using Application.Exceptions;
using Application.Features.User_Features.User.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.User_Features.User.Handlers.Queries
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserRequest, List<UserResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDTO>> Handle(
            GetAllUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var users = await _unitOfWork.UserRepository.GetAll(
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
            var userResponse = _mapper.Map<List<UserResponseDTO>>(users);
            return userResponse;
        }
    }
}
