using Application.Contracts.Persistance.Repositories;
using Application.DTO.Product.ColorDTO.DTO;
using Application.Exceptions;
using Application.Features.Product_Features.Color.Requests.Queries;
using AutoMapper;
using MediatR;



namespace Application.Features.Product_Features.Color.Handlers.Queries
{
    public class GetAllColorHandler : IRequestHandler<GetAllColor, List<ColorResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ColorResponseDTO>> Handle(GetAllColor request, CancellationToken cancellationToken)
        {
            var Colors = await _unitOfWork.ColorRepository.GetAll();
            if (Colors == null)
            {
                throw new NotFoundException("No Colors found");
            }
            var ColorResponse = _mapper.Map<List<ColorResponseDTO>>(Colors);
            return ColorResponse;
        }
    }
}