using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.BrandDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Brand.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Brand.Handlers.Queries
{
    public class GetAllBrandHandler : IRequestHandler<GetAllBrand, List<BrandResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BrandResponseDTO>> Handle(
            GetAllBrand request,
            CancellationToken cancellationToken
        )
        {
            var Brands = await _unitOfWork.BrandRepository.GetAll();
            if (Brands == null)
            {
                throw new NotFoundException("No Brands found");
            }
            var BrandResponse = _mapper.Map<List<BrandResponseDTO>>(Brands);
            return BrandResponse;
        }
    }
}
