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
    public class DeleteBrandHandler
        : IRequestHandler<DeleteBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public DeleteBrandHandler(
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
            DeleteBrandRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id.Length == 0)
                throw new BadRequestException("Invalid Brand Id");

            var Brand = await _unitOfWork.BrandRepository.GetById(request.Id);

            if (Brand == null)
                throw new NotFoundException("Brand Not Found");

            var isDeleted = await _imageRepository.Delete(Brand.Id);
            if (!isDeleted)
                throw new BadRequestException("Failed to delete Brand");

            await _unitOfWork.BrandRepository.Delete(Brand);

            return new BaseResponse<BrandResponseDTO>
            {
                Message = "Brand Deleted Successfully",
                Success = true,
                Data = _mapper.Map<BrandResponseDTO>(Brand)
            };
        }
    }
}
