using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Size.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Handlers.Queries
{
    public class GetSizeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetSizeById, SizeResponseDTO>
    {
        public async Task<SizeResponseDTO> Handle(GetSizeById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Size = await unitOfWork.SizeRepository.GetById(request.Id);
            if (Size == null)
            {
                throw new NotFoundException("Size with that {request.Id} does not exist");
            }
            var SizeResponse = mapper.Map<SizeResponseDTO>(Size);
            return SizeResponse;
        }

    }
}