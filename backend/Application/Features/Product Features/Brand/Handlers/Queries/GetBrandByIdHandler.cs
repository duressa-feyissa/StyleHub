using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Brand.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Handlers.Queries
{
    public class GetBrandByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetBrandById, BrandResponseDTO>
    {
        public async Task<BrandResponseDTO> Handle(GetBrandById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Brand = await unitOfWork.BrandRepository.GetById(request.Id);
            if (Brand == null)
            {
                throw new NotFoundException("Brand with that {request.Id} does not exist");
            }
            var BrandResponse = mapper.Map<BrandResponseDTO>(Brand);
            return BrandResponse;
        }

    }
}