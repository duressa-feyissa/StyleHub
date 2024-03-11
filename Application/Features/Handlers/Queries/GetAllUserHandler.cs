using AutoMapper;
using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.Contracts;
using SytleHub.Application.Exceptions;

namespace StyleHub.Application.Features.Requests.Queries
{
    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseDTO>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUserHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserResponseDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAllUser();
            if (users == null)
                throw new NotFoundException("User not found");

            return _mapper.Map<List<UserResponseDTO>>(users);
        }
    }
}