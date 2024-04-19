using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Size.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Size.Handlers.Queries
{
    public class GetAllSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllSize, List<SizeResponseDTO>>
    {
        public async Task<List<SizeResponseDTO>> Handle(GetAllSize request, CancellationToken cancellationToken)
        {
            var Sizes = await unitOfWork.SizeRepository.GetAll();
            if (Sizes == null)
            {
                throw new NotFoundException("No Sizes found");
            }
            var SizeResponse = mapper.Map<List<SizeResponseDTO>>(Sizes);
            return SizeResponse;
        }
    }
}