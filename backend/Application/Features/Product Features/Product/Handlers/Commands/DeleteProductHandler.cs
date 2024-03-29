using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ProductDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Product.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product_Features.Product.Requests.Handlers.Commands
{
    public class DeleteProductHandler
        : IRequestHandler<DeleteProductRequest, BaseResponse<ProductResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageUploadRepository _imageUploadRepository;

        public DeleteProductHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageUploadRepository imageUploadRepository
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageUploadRepository = imageUploadRepository;
        }

        public async Task<BaseResponse<ProductResponseDTO>> Handle(
            DeleteProductRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id.Length == 0)
                throw new BadRequestException("Invalid Product Id");

            var product = await _unitOfWork.ProductRepository.GetById(request.Id);

            if (product == null)
                throw new NotFoundException("Product Not Found");

            foreach (var image in product.Images)
                await _imageUploadRepository.Delete(image.Id);

            await _unitOfWork.ProductRepository.Delete(product);

            return new BaseResponse<ProductResponseDTO>
            {
                Message = "Product Deleted Successfully",
                Success = true,
                Data = _mapper.Map<ProductResponseDTO>(product)
            };
        }
    }
}
