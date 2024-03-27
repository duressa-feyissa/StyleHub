using Application.Contracts;
using Application.DTO.ProductDTO.DTO;
using Application.Exceptions;
using Application.Features.Product.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product.Handlers
{

    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, BaseResponse<ProductResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<ProductResponseDTO>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Product Id");
            }

            var Product = await _unitOfWork.ProductRepository.GetById(request.Id);

            if (Product == null)
            {
                throw new NotFoundException("Product Not Found");
            }

            await _unitOfWork.ProductRepository.Delete(Product);

            return new BaseResponse<ProductResponseDTO>
            {
                Message = "Product Deleted Successfully",
                Success = true,
                Data = _mapper.Map<ProductResponseDTO>(Product)
            };

        }
    }

}