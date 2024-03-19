using Application.Contracts;
using Application.DTO.ProductDTO.DTO;
using Application.DTO.ProductDTO.Validations;
using Application.Exceptions;
using Application.Features.Product.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Product.Handlers
{

    public class CreateProductHandler : IRequestHandler<CreateProductRequest, BaseResponse<ProductResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<ProductResponseDTO>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseProductValidation();
            var validationResult = await validator.ValidateAsync(request.Product!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid Product Data");
            }

            var product = _mapper.Map<Domain.Entities.Product>(request.Product);
            await _unitOfWork.ProductRepository.Add(product);

            return new BaseResponse<ProductResponseDTO>
            {
                Message = "Product Created Successfully",
                Success = true,
                Data = _mapper.Map<ProductResponseDTO>(product)
            };


        }
    }

}