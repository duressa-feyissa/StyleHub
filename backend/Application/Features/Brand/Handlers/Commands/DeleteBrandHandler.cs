using Application.Contracts;
using Application.DTO.BrandDTO.DTO;
using Application.Exceptions;
using Application.Features.Brand.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Brand.Handlers
{

    public class DeleteBrandHandler : IRequestHandler<DeleteBrandRequest, BaseResponse<BrandResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<BrandResponseDTO>> Handle(DeleteBrandRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Brand Id");
            }

            var Brand = await _unitOfWork.BrandRepository.GetById(request.Id);

            if (Brand == null)
            {
                throw new  NotFoundException("Brand Not Found");
            }

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