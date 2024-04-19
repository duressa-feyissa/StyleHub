using AutoMapper;
using backend.Application.Contracts.Infrastructure.Repositories;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.BrandDTO.DTO;
using backend.Application.DTO.Product.BrandDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Brand.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Brand.Handlers.Commands
{
    public class CreateBrandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IImageRepository imageRepository)
        : IRequestHandler<CreateBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        public async Task<BaseResponse<BrandResponseDTO>> Handle(
            CreateBrandRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateBrandValidation(unitOfWork.BrandRepository);
            var validationResult = await validator.ValidateAsync(request.Brand!);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);

            var Brand = mapper.Map<Domain.Entities.Product.Brand>(request.Brand);
            Brand.Logo = await imageRepository.Upload(Brand.Logo, Brand.Id);
            await unitOfWork.BrandRepository.Add(Brand);

            return new BaseResponse<BrandResponseDTO>
            {
                Message = "Brand Created Successfully",
                Success = true,
                Data = mapper.Map<BrandResponseDTO>(Brand)
            };
        }
    }
}
