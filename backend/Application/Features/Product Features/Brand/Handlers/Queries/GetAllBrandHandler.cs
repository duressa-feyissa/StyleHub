using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Brand.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Handlers.Queries
{
    public class GetAllBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllBrand, List<BrandResponseDTO>>
    {
        public async Task<List<BrandResponseDTO>> Handle(
            GetAllBrand request,
            CancellationToken cancellationToken
        )
        {
            var Brands = await unitOfWork.BrandRepository.GetAll();
            if (Brands == null)
            {
                throw new NotFoundException("No Brands found");
            }
            var BrandResponse = mapper.Map<List<BrandResponseDTO>>(Brands);
            return BrandResponse;
        }
    }
}
