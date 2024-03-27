using Application.Contracts;
using Application.DTO.BrandDTO.DTO;
using Application.DTO.BrandDTO.Validations;
using Application.Exceptions;
using Application.Features.Brand.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Application.Features.Brand.Handlers
{

    public class UpdateBrandHandler : IRequestHandler<UpdateBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<BrandResponseDTO>> Handle(UpdateBrandRequest request, CancellationToken cancellationToken)
        {
            var validator = new BaseBrandValidation(_unitOfWork.BrandRepository);
            var validationResult = await validator.ValidateAsync(request.Brand!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var existingBrand = await _unitOfWork.BrandRepository.GetById(request.Id);

            if (existingBrand == null)
            {
                throw new NotFoundException("Brand Not Found");
            }

            existingBrand.UpdatedAt = DateTime.Now;

            var Brand = _mapper.Map(request.Brand, existingBrand);
            await _unitOfWork.BrandRepository.Update(Brand);
            return new BaseResponse<BrandResponseDTO>
            {
                Message = "Brand Updated Successfully",
                Success = true,
                Data = _mapper.Map<BrandResponseDTO>(Brand)
            };
        }
    }
}