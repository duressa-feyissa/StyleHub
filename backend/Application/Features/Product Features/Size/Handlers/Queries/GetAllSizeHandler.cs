using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.SizeDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Size.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Size.Handlers.Queries
{
    public class GetAllSizeHandler : IRequestHandler<GetAllSize, List<SizeResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSizeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<SizeResponseDTO>> Handle(GetAllSize request, CancellationToken cancellationToken)
        {
            var Sizes = await _unitOfWork.SizeRepository.GetAll();
            if (Sizes == null)
            {
                throw new NotFoundException("No Sizes found");
            }
            var SizeResponse = _mapper.Map<List<SizeResponseDTO>>(Sizes);
            return SizeResponse;
        }
    }
}