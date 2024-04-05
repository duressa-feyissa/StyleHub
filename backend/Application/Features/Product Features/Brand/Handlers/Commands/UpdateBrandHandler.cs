using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.BrandDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Brand.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Brand.Handlers.Commands
{
    public class UpdateBrandHandler
        : IRequestHandler<UpdateBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public UpdateBrandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageRepository imageRepository
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageRepository = imageRepository;
        }

        public async Task<BaseResponse<BrandResponseDTO>> Handle(
            UpdateBrandRequest request,
            CancellationToken cancellationToken
        )
        {
            var existingBrand = await _unitOfWork.BrandRepository.GetById(request.Id);
            if (existingBrand == null)
                throw new NotFoundException("Brand Not Found");

            if (request?.Brand?.Name != null)
            {
                var existingBrandName = await _unitOfWork.BrandRepository.GetByName(
                    request.Brand.Name
                );
                if (existingBrandName != null && existingBrandName.Id != existingBrand.Id)
                    throw new BadRequestException("Brand Name Already Exists");
                if (request.Brand.Name.Trim().Length < 2)
                    throw new BadRequestException("Brand Name Must Be At Least 2 Characters");
                existingBrand.Name = request.Brand.Name.ToLower();
            }

            if (request?.Brand?.Logo != null)
                existingBrand.Logo = await _imageRepository.Update(
                    request.Brand.Logo,
                    existingBrand.Id
                );

            if (request?.Brand?.Country != null)
                existingBrand.Country = request.Brand.Country.ToLower();

            existingBrand.UpdatedAt = DateTime.Now;
            await _unitOfWork.BrandRepository.Update(existingBrand);

            return new BaseResponse<BrandResponseDTO>
            {
                Message = "Brand Updated Successfully",
                Success = true,
                Data = _mapper.Map<BrandResponseDTO>(existingBrand)
            };
        }
    }
}
