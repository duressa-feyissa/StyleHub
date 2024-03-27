using Application.DTO.BrandDTO.DTO;
using Application.Features.Brand.Requests.Queries;
using MediatR;
using AutoMapper;
using Application.Contracts;
using Application.Exceptions;

namespace Application.Features.Brand.Handlers.Queries
{
    public class GetBrandByIdHandler : IRequestHandler<GetBrandById, BrandResponseDTO>
    {

        private readonly IMapper _mapper;


        private readonly IUnitOfWork _unitOfWork;

        public GetBrandByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BrandResponseDTO> Handle(GetBrandById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Brand = await _unitOfWork.BrandRepository.GetById(request.Id);
            if (Brand == null)
            {
                throw new NotFoundException("Brand with that {request.Id} does not exist");
            }
            var BrandResponse = _mapper.Map<BrandResponseDTO>(Brand);
            return BrandResponse;
        }

    }
}