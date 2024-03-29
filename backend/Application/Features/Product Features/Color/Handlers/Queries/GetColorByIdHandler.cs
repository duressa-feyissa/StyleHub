using MediatR;
using AutoMapper;
using Application.Exceptions;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ColorDTO.DTO;
using Application.Features.Product_Features.Color.Requests.Queries;

namespace Application.Features.Product_Features.Color.Handlers.Queries
{
    public class GetColorByIdHandler : IRequestHandler<GetColorById, ColorResponseDTO>
    {

        private readonly IMapper _mapper;


        private readonly IUnitOfWork _unitOfWork;

        public GetColorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ColorResponseDTO> Handle(GetColorById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Color = await _unitOfWork.ColorRepository.GetById(request.Id);
            if (Color == null)
            {
                throw new NotFoundException("Color with that {request.Id} does not exist");
            }
            var ColorResponse = _mapper.Map<ColorResponseDTO>(Color);
            return ColorResponse;
        }

    }
}