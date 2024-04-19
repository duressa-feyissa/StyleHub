using AutoMapper;
using backend.Application.Contracts.Infrastructure.Repositories;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Brand.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Handlers.Commands
{
    public class DeleteBrandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IImageRepository imageRepository)
        : IRequestHandler<DeleteBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        public async Task<BaseResponse<BrandResponseDTO>> Handle(
            DeleteBrandRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id.Length == 0)
                throw new BadRequestException("Invalid Brand Id");

            var Brand = await unitOfWork.BrandRepository.GetById(request.Id);

            if (Brand == null)
                throw new NotFoundException("Brand Not Found");

            var isDeleted = await imageRepository.Delete(Brand.Id);
            if (!isDeleted)
                throw new BadRequestException("Failed to delete Brand");

            await unitOfWork.BrandRepository.Delete(Brand);

            return new BaseResponse<BrandResponseDTO>
            {
                Message = "Brand Deleted Successfully",
                Success = true,
                Data = mapper.Map<BrandResponseDTO>(Brand)
            };
        }
    }
}
