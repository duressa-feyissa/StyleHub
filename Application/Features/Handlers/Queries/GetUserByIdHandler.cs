using AutoMapper;
using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.Contracts;
using SytleHub.Application.Exceptions;

namespace StyleHub.Application.Features.Requests.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponseDTO>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponseDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id.Length == 0)
                throw new BadRequestException("Id is required");

            var user = await _unitOfWork.UserRepository.GetUserById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}