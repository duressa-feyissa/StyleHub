using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ColorDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Product_Features.Color.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Color.Handlers.Queries
{
    public class GetColorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetColorById, ColorResponseDTO>
    {
        public async Task<ColorResponseDTO> Handle(GetColorById request, CancellationToken cancellationToken)
        {
            if (request.Id == null || request.Id.Length == 0)
            {
                throw new BadRequestException("Id is required");
            }

            var Color = await unitOfWork.ColorRepository.GetById(request.Id);
            if (Color == null)
            {
                throw new NotFoundException("Color with that {request.Id} does not exist");
            }
            var ColorResponse = mapper.Map<ColorResponseDTO>(Color);
            return ColorResponse;
        }

    }
}