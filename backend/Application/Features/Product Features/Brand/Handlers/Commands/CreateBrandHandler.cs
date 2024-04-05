using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.BrandDTO.DTO;
using Application.DTO.Product.BrandDTO.Validations;
using Application.Exceptions;
using Application.Features.Product_Features.Brand.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Brand.Handlers.Commands
{
    public class CreateBrandHandler
        : IRequestHandler<CreateBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public CreateBrandHandler(
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
            CreateBrandRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new CreateBrandValidation(_unitOfWork.BrandRepository);
            var validationResult = await validator.ValidateAsync(request.Brand!);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);

            var Brand = _mapper.Map<Domain.Entities.Product.Brand>(request.Brand);
            Brand.Logo = await _imageRepository.Upload(Brand.Logo, Brand.Id);
            await _unitOfWork.BrandRepository.Add(Brand);

            return new BaseResponse<BrandResponseDTO>
            {
                Message = "Brand Created Successfully",
                Success = true,
                Data = _mapper.Map<BrandResponseDTO>(Brand)
            };
        }
    }
}
