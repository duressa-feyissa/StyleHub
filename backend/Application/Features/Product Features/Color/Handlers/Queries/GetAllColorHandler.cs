using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Color.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Handlers.Queries
{
    public class GetAllColorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllColor, List<ColorResponseDTO>>
    {
        public async Task<List<ColorResponseDTO>> Handle(GetAllColor request, CancellationToken cancellationToken)
        {
            var Colors = await unitOfWork.ColorRepository.GetAll();
            if (Colors == null)
            {
                throw new NotFoundException("No Colors found");
            }
            var ColorResponse = mapper.Map<List<ColorResponseDTO>>(Colors);
            return ColorResponse;
        }
    }
}