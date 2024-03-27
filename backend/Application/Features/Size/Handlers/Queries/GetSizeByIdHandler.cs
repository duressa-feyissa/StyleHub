using Application.DTO.SizeDTO.DTO;
using Application.Features.Size.Requests.Queries;
using MediatR;
using AutoMapper;
using Application.Contracts;
using Application.Exceptions;

namespace Application.Features.Size.Handlers.Queries
{
    public class GetSizeByIdHandler : IRequestHandler<GetSizeById, SizeResponseDTO>
    {

        private readonly IMapper _mapper;


        private readonly IUnitOfWork _unitOfWork;

        public GetSizeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SizeResponseDTO> Handle(GetSizeById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Size = await _unitOfWork.SizeRepository.GetById(request.Id);
            if (Size == null)
            {
                throw new NotFoundException("Size with that {request.Id} does not exist");
            }
            var SizeResponse = _mapper.Map<SizeResponseDTO>(Size);
            return SizeResponse;
        }

    }
}