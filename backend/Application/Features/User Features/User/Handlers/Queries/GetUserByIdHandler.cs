using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.UserDTO.DTO;
using Application.Exceptions;
using Application.Features.User_Features.User.Requests.Command;
using Application.Features.User_Features.User.Requests.Queries;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.User_Features.User.Handlers.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(
            GetUserByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
