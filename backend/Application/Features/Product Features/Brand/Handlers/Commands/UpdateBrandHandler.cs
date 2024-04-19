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
    public class UpdateBrandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IImageRepository imageRepository)
        : IRequestHandler<UpdateBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        public async Task<BaseResponse<BrandResponseDTO>> Handle(
            UpdateBrandRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingBrand = await unitOfWork.BrandRepository.GetById(request.Id);
            if (existingBrand == null)
                throw new NotFoundException("Brand Not Found");

            if (request?.Brand?.Name != null)
            {
                var existingBrandName = await unitOfWork.BrandRepository.GetByName(
                    request.Brand.Name
                );
                if (existingBrandName != null && existingBrandName.Id != existingBrand.Id)
                    throw new BadRequestException("Brand Name Already Exists");
                if (request.Brand.Name.Trim().Length < 2)
                    throw new BadRequestException("Brand Name Must Be At Least 2 Characters");
                existingBrand.Name = request.Brand.Name.ToLower();
            }

            if (request?.Brand?.Logo != null)
                existingBrand.Logo = await imageRepository.Update(
                    request.Brand.Logo,
                    existingBrand.Id
                );

            if (request?.Brand?.Country != null)
                existingBrand.Country = request.Brand.Country.ToLower();

            existingBrand.UpdatedAt = DateTime.Now;
            await unitOfWork.BrandRepository.Update(existingBrand);

            return new BaseResponse<BrandResponseDTO>
            {
                Message = "Brand Updated Successfully",
                Success = true,
                Data = mapper.Map<BrandResponseDTO>(existingBrand)
            };
        }
    }
}
