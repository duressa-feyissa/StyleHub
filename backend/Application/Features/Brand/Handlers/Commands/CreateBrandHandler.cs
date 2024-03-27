using Application.Contracts;
using Application.DTO.BrandDTO.DTO;
using Application.DTO.BrandDTO.Validations;
using Application.Exceptions;
using Application.Features.Brand.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Brand.Handlers
{

    public class CreateBrandHandler : IRequestHandler<CreateBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<BrandResponseDTO>> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseBrandValidation(_unitOfWork.BrandRepository);
            var validationResult = await validator.ValidateAsync(request.Brand!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var Brand = _mapper.Map<Domain.Entities.Brand>(request.Brand);
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