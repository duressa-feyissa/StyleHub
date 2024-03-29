using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Location.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Location.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Location.Handlers.Commands
{

    public class DeleteLocationHandler : IRequestHandler<DeleteLocationRequest, BaseResponse<LocationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }


        public async Task<BaseResponse<LocationResponseDTO>> Handle(DeleteLocationRequest request, CancellationToken cancellationToken)
        {

            if (request.Id.Length == 0)
            {
                throw new BadRequestException("Invalid Location Id");
            }

            var Location = await _unitOfWork.LocationRepository.GetById(request.Id);

            if (Location == null)
            {
                throw new  NotFoundException("Location Not Found");
            }

            await _unitOfWork.LocationRepository.Delete(Location);

            return new BaseResponse<LocationResponseDTO>
            {
                Message = "Location Deleted Successfully",
                Success = true,
                Data = _mapper.Map<LocationResponseDTO>(Location)
            };

        }
    }

}